#!/bin/bash

# --- Configuration ---
APP_NAME="flexicmms"
APP_DIR="/var/www/flexicmms"
APP_ZIP="${APP_NAME}.zip"
APP_DLL="BlazorTool.dll" # Main application DLL
SERVICE_NAME="${APP_NAME}.service"
SERVICE_FILE="/etc/systemd/system/${SERVICE_NAME}"
APPSETTINGS_FILE="${APP_DIR}/appsettings.json"
NGINX_CONF_NAME="flexicmms"
NGINX_SITES_AVAILABLE="/etc/nginx/sites-available/${NGINX_CONF_NAME}"
NGINX_SITES_ENABLED="/etc/nginx/sites-enabled/${NGINX_CONF_NAME}"
KESTREL_INTERNAL_PORT="5000" # Internal port Kestrel will listen on

# --- Functions ---
# Function to check if a command exists
command_exists () {
  type "$1" &> /dev/null ;
}

# Function to prompt for API address with trailing slash check
prompt_api_url() {
  local api_url_input
  read -p "Enter the actual API server address (e.g., http://10.1.40.128:1122/api/v1/): " api_url_input
  if [[ "${api_url_input}" != */ ]]; then
    api_url_input="${api_url_input}/"
    echo "Trailing slash added: ${api_url_input}"
  fi
  echo "${api_url_input}"
}

# Function to prompt for domain/IP for Nginx
prompt_nginx_domain() {
  local domain_input
  read -p "Enter the domain name or IP address for Nginx (e.g., example.com or 192.168.1.100): " domain_input
  echo "${domain_input}"
}

# --- Main deployment steps ---

echo "Starting FlexiCMMS deployment on Linux server with Nginx..."

# 1. Check for superuser privileges
if [ "$EUID" -ne 0 ]; then
  echo "Error: Please run the script with superuser privileges (sudo)."
  exit 1
fi

# 2. Check for and install unzip and nginx
if ! command_exists unzip; then
  echo "Installing unzip..."
  apt update && apt install -y unzip
fi

if ! command_exists nginx; then
  echo "Installing Nginx..."
  apt update && apt install -y nginx
fi

# 3. Install .NET Runtime 8.0
echo "Step 1: Installing .NET Runtime 8.0..."
apt update
apt install -y dotnet-runtime-8.0

# 4. Create directory and unpack application
echo "Step 2: Creating directory and unpacking application..."
mkdir -p "${APP_DIR}"

if [ ! -f "${APP_ZIP}" ]; then
  echo "Error: ZIP archive '${APP_ZIP}' not found in the current directory."
  echo "Please copy '${APP_ZIP}' to the same directory as this script, or provide the full path."
  exit 1
fi
unzip "${APP_ZIP}" -d "${APP_DIR}"
chown -R www-data:www-data "${APP_DIR}"
chmod -R 755 "${APP_DIR}"

# 5. Configure appsettings.json
echo "Step 3: Configuring appsettings.json..."
EXTERNAL_API_BASE_URL=$(prompt_api_url)

# Form the full content of appsettings.json, including the entered API address
APPSETTINGS_CONTENT_TO_WRITE='{
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "ExternalApi": {
        "BasicAuthUsername": "FlexiUser",
        "BasicAuthPassword": "Flexi2018",
        "BaseUrl": "'"${EXTERNAL_API_BASE_URL}"'" // Specify your actual API address
      },
      "AllowedHosts": "*"
    }'

echo "${APPSETTINGS_CONTENT_TO_WRITE}" > "${APPSETTINGS_FILE}"
echo "File ${APPSETTINGS_FILE} updated."

# 6. Create Systemd service
echo "Step 4: Creating Systemd service..."
cat <<EOF > "${SERVICE_FILE}"
[Unit]
Description=FlexiCMMS Application
After=network.target

[Service]
WorkingDirectory=${APP_DIR}
ExecStart=/usr/bin/dotnet ${APP_DIR}/${APP_DLL}
Restart=always
# Restart service after 10 seconds if the dotnet process crashes
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=flexicmms
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production
Environment=ASPNETCORE_URLS=http://localhost:${KESTREL_INTERNAL_PORT} # Kestrel listens on an internal port
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
EOF

echo "Service file ${SERVICE_FILE} created."

# 7. Configure Nginx
echo "Step 5: Configuring Nginx..."
NGINX_DOMAIN_OR_IP=$(prompt_nginx_domain)

cat <<EOF > "${NGINX_SITES_AVAILABLE}"
# Map для корректной работы WebSockets (SignalR в Blazor Server)
map \$http_upgrade \$connection_upgrade {
    default Upgrade;
    ''      close;
}

server {
    listen 80;
    listen [::]:80;
    server_name ${NGINX_DOMAIN_OR_IP};

    location / {
        proxy_pass http://localhost:${KESTREL_INTERNAL_PORT};
        proxy_http_version 1.1;

        # Обязательные заголовки
        proxy_set_header Host \$host;
        proxy_set_header X-Forwarded-For \$proxy_add_x_forwarded_for;
        proxy_set_header X-Forwarded-Proto \$scheme;
        proxy_set_header X-Forwarded-Host \$host;

        # Для SignalR / WebSockets
        proxy_set_header Upgrade \$http_upgrade;
        proxy_set_header Connection \$connection_upgrade;

        # Отключаем буферизацию для стриминга и SignalR
        proxy_buffering off;

        # Увеличиваем таймауты для долгих соединений
        proxy_read_timeout 1h;
        proxy_send_timeout 1h;
    }

    # Если вы используете HTTPS, раскомментируйте и настройте этот раздел (пример):
    # listen 443 ssl;
    # listen [::]:443 ssl;
    # ssl_certificate /etc/letsencrypt/live/${NGINX_DOMAIN_OR_IP}/fullchain.pem;
    # ssl_certificate_key /etc/letsencrypt/live/${NGINX_DOMAIN_OR_IP}/privkey.pem;
    # include /etc/letsencrypt/options-ssl-nginx.conf;
    # ssl_dhparam /etc/letsencrypt/ssl-dhparams.pem;
}
EOF

echo "Nginx configuration file ${NGINX_SITES_AVAILABLE} created."

# 8. Activate Nginx configuration
if [ -f "${NGINX_SITES_ENABLED}" ]; then
  echo "Removing existing Nginx symbolic link..."
  rm "${NGINX_SITES_ENABLED}"
fi

ln -s "${NGINX_SITES_AVAILABLE}" "${NGINX_SITES_ENABLED}"

echo "Nginx configuration activated."

# 9. Test Nginx configuration and reload
echo "Testing Nginx configuration and reloading..."
nginx -t
systemctl restart nginx

# 10. Start and enable the Systemd service
echo "Step 6: Starting and enabling the service..."
systemctl daemon-reload
systemctl start "${APP_NAME}"
systemctl enable "${APP_NAME}"

echo "Checking status of service ${APP_NAME}..."
systemctl status "${APP_NAME}" --no-pager

echo "FlexiCMMS deployment with Nginx completed!"
echo "Your application should be accessible via your server's domain name or IP address."
echo "Ensure that port 80 (and 443 if using HTTPS) are open in your server's firewall."
echo "If you plan to use HTTPS, remember to configure SSL certificates in Nginx (e.g., with Certbot)."
