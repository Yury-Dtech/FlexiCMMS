#!/bin/bash

# --- Configuration ---
APP_NAME="flexicmms"
APP_DIR="/var/www/flexicmms"
APP_ZIP="${APP_NAME}.zip"
APP_DLL="BlazorTool.dll" # Main application DLL
SERVICE_NAME="${APP_NAME}.service"
SERVICE_FILE="/etc/systemd/system/${SERVICE_NAME}"
APPSETTINGS_FILE="${APP_DIR}/appsettings.json"
PFX_FILE="${APP_DIR}/blazortool.pfx" # Path to the PFX certificate file

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

# --- Main deployment steps ---

echo "Starting FlexiCMMS deployment on Linux server..."

# 1. Check for superuser privileges
if [ "$EUID" -ne 0 ]; then
  echo "Error: Please run the script with superuser privileges (sudo)."
  exit 1
fi

# 2. Check for and install unzip
if ! command_exists unzip; then
  echo "Installing unzip..."
  apt update && apt install -y unzip
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
  echo "Please copy '${APP_ZIP}' to the same directory as this script, or provide the full path." # Added clarification
  exit 1
fi
unzip "${APP_ZIP}" -d "${APP_DIR}"
chown -R www-data:www-data "${APP_DIR}"
chmod -R 755 "${APP_DIR}"

# 5. Check for PFX file presence
if [ ! -f "${PFX_FILE}" ]; then
  echo "Error: PFX file '${PFX_FILE}' not found."
  echo "Please copy 'blazortool.pfx' to the '${APP_DIR}' directory before running the script."
  exit 1
fi

# 6. Configure appsettings.json
echo "Step 3: Configuring appsettings.json..."
EXTERNAL_API_BASE_URL=$(prompt_api_url)

# Form the full content of appsettings.json, including the entered API address
APPSETTINGS_CONTENT_TO_WRITE='''{
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "ExternalApi": {
        "BasicAuthUsername": "FlexiUser",
        "BasicAuthPassword": "Flexi2018",
        "BaseUrl": "'''"${EXTERNAL_API_BASE_URL}"'''" // Specify your actual API address
      },
      "AllowedHosts": "*",
      "Kestrel": {
        "Certificates": {
          "Default": {
            "Path": "/var/www/flexicmms/blazortool.pfx",
            "Password": "dtech"
          }
        }
      }
    }'''

echo "${APPSETTINGS_CONTENT_TO_WRITE}" > "${APPSETTINGS_FILE}"
echo "File ${APPSETTINGS_FILE} updated."

# 7. Create Systemd service
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
Environment="ASPNETCORE_ENVIRONMENT=Production"
Environment="ASPNETCORE_URLS=http://+:80;https://+:443"
Environment=ASPNETCORE_Kestrel__Certificates__Default__Path=${PFX_FILE}
Environment=ASPNETCORE_Kestrel__Certificates__Default__Password=dtech
Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

[Install]
WantedBy=multi-user.target
EOF

echo "Service file ${SERVICE_FILE} created."

# 8. Start and enable the service
echo "Step 5: Starting and enabling the service..."
systemctl daemon-reload
systemctl start "${APP_NAME}"
systemctl enable "${APP_NAME}"

echo "Checking status of service ${APP_NAME}..."
systemctl status "${APP_NAME}" --no-pager

echo "FlexiCMMS deployment completed!"
echo "Your application should be accessible via HTTP on port 80 and HTTPS on port 443."
echo "Ensure that ports 80 and 443 are open in your server's firewall."
echo "If you are using a self-signed certificate, browsers will show security warnings."