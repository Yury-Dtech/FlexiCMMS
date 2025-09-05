using Blazored.LocalStorage;
using BlazorTool.Client.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace BlazorTool.Client.Services
{
    public class UserState
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly ILogger<UserState> _logger;
        public Task InitializationTask { get; private set; }
        public event Action OnChange;

        public UserState(ILocalStorageService localStorageService, ILogger<UserState> logger)
        {
            _localStorageService = localStorageService;
            _logger = logger;
            InitializationTask = LoadIdentityDataFromCacheAsync();
        }


        public UserState()
        {
            //_localStorageService = new Blazored.LocalStorage.LocalStorageService();
            InitializationTask = Task.CompletedTask; // For parameterless constructor, if ever used
        }

        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string LangCode { get; set; } = "pl-pl"; 
        public string? Token { get; set; }
        public int? PersonID { get; set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(UserName);
        public RightMatrix? RightMatrix { get; set; }
        public int? RightMatrixID { get; set; } //1 - admin, 2 - power user. 3. user
        public int? DepartmentID { get; set; }
        public bool UseOriginalColors { get; set; } = true;
        public bool CanHaveManyActiveTake { get; set; } = false;
        public string? NetworkShareUsername { get; set; }
        public string? NetworkSharePassword { get; set; }
        public string? NetworkShareServer { get; set; }

        public bool HasPermission(PermissionType permission)
        {
            if (RightMatrix == null)
            {
                return false;
            }

            return permission switch
            {
                PermissionType.WO_Add => RightMatrix.WO_Add,
                PermissionType.WO_Edit => RightMatrix.WO_Edit,
                PermissionType.WO_Edit_Description => RightMatrix.WO_Edit_Description,
                PermissionType.WO_Del => RightMatrix.WO_Del,
                PermissionType.WO_Close => RightMatrix.WO_Close,
                PermissionType.AcT_Add => RightMatrix.AcT_Add,
                PermissionType.AcT_Del => RightMatrix.AcT_Del,
                PermissionType.AcT_Edit_Description => RightMatrix.AcT_Edit_Description,
                PermissionType.ParT_WO_take => RightMatrix.ParT_WO_take,
                PermissionType.ParT_WO_Order => RightMatrix.ParT_WO_Order,
                PermissionType.ParT_Edit_State => RightMatrix.ParT_Edit_State,
                PermissionType.WO_SET_AssignedPerson => RightMatrix.WO_SET_AssignedPerson,
                PermissionType.ParT_Give => RightMatrix.ParT_Give,
                PermissionType.MD_Add => RightMatrix.MD_Add,
                PermissionType.MD_Edit => RightMatrix.MD_Edit,
                PermissionType.MD_Edit_Warranty => RightMatrix.MD_Edit_Warranty,
                PermissionType.ParT_Add => RightMatrix.ParT_Add,
                PermissionType.MD_Add_ForceCycle => RightMatrix.MD_Add_ForceCycle,
                PermissionType.ADMIN => RightMatrixID == 1,
                _ => false,
            };
        }

        public async Task<bool> SaveIdentityDataToCacheAsync(IdentityData identityData)
        {
            _logger.LogInformation("DEBUG: SaveIdentityDataAsync started. LangCode from data: {LangCode}", identityData.LangCode);
            UserName = identityData.Name;
            Token = identityData.Token;
            PersonID = identityData.PersonID;
            LangCode = identityData.LangCode;
            RightMatrix = identityData.RigthMatrix;
            RightMatrixID = identityData.RigthMatrixID;
            DepartmentID = identityData.DepartmentID;
            UseOriginalColors = identityData.UseOriginalColors;
            CanHaveManyActiveTake = identityData.CanHaveManyActiveTake;
            NetworkShareUsername = identityData.NetworkShareUsername;
            NetworkSharePassword = identityData.NetworkSharePassword;
            NetworkShareServer = identityData.NetworkShareServer;
            var cultureInfo = new CultureInfo(identityData.LangCode);
            bool isForceReload = identityData.LangCode != CultureInfo.CurrentCulture.Name;
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            _logger.LogInformation("DEBUG: Culture set to {CultureName} in SaveIdentityDataAsync.", cultureInfo.Name);
            await _localStorageService.SetItemAsStringAsync("identityData", JsonConvert.SerializeObject(identityData));
            NotifyStateChanged();
            return isForceReload;
        }

        public async Task<bool> SwitchLanguage(string langCode)
        {
            bool isForceReload = langCode != CultureInfo.CurrentCulture.Name;
            LangCode = langCode;
            await SaveToLocalStorage();
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(langCode);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(langCode);
            OnChange?.Invoke();
            return isForceReload;
        }

        public async Task SaveToLocalStorage()
        {
            IdentityData data = new IdentityData
            {
                Name = this.UserName,
                Token = this.Token,
                PersonID = this.PersonID ?? 0,
                LangCode = this.LangCode,
                RigthMatrix = this.RightMatrix,
                UseOriginalColors = this.UseOriginalColors,
                NetworkShareUsername = this.NetworkShareUsername,
                NetworkSharePassword = this.NetworkSharePassword,
                NetworkShareServer = this.NetworkShareServer
            };
            await _localStorageService.SetItemAsStringAsync("identityData", JsonConvert.SerializeObject(data));
        }
        public async Task LoadFromIdentityData(IdentityData? identityData)
        {
            if (identityData != null)
            {
                UserName = identityData.Name;
                Token = identityData.Token;
                PersonID = identityData.PersonID;
                LangCode = identityData.LangCode;
                RightMatrix = identityData.RigthMatrix;
                UseOriginalColors = identityData.UseOriginalColors;
                CanHaveManyActiveTake = identityData.CanHaveManyActiveTake;
                NetworkShareUsername = identityData.NetworkShareUsername;
                NetworkSharePassword = identityData.NetworkSharePassword;
                NetworkShareServer = identityData.NetworkShareServer;
                var cultureInfo = new CultureInfo(identityData.LangCode);
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                await SaveIdentityDataToCacheAsync(identityData);
            }
        }

        public async Task<bool> LoadIdentityDataFromCacheAsync()
        {
            _logger.LogInformation("DEBUG: LoadIdentityDataAsync started.");
            string? identityDataJson = await _localStorageService.GetItemAsStringAsync("identityData");
            if (!string.IsNullOrEmpty(identityDataJson))
            {
                IdentityData? identityData = JsonConvert.DeserializeObject<IdentityData>(identityDataJson);
                bool isForceReload = false;
                if (identityData != null)
                {
                    _logger.LogInformation("DEBUG: Loaded LangCode from storage: {LangCode}", identityData.LangCode);
                    UserName = identityData.Name;
                    Token = identityData.Token;
                    PersonID = identityData.PersonID;
                    LangCode = identityData.LangCode;
                    RightMatrix = identityData.RigthMatrix;
                    UseOriginalColors = identityData.UseOriginalColors;
                    CanHaveManyActiveTake = identityData.CanHaveManyActiveTake;
                    NetworkShareUsername = identityData.NetworkShareUsername;
                    NetworkSharePassword = identityData.NetworkSharePassword;
                    NetworkShareServer = identityData.NetworkShareServer;
                    isForceReload = identityData.LangCode != CultureInfo.CurrentCulture.Name;
                    var cultureInfo = new CultureInfo(identityData.LangCode);
                    CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                    CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
                    _logger.LogInformation("DEBUG: Culture set to {CultureName} in LoadIdentityDataAsync.", cultureInfo.Name);
                }
                    return isForceReload;
            }
            else
            {
                _logger.LogWarning("DEBUG: No identityData found in local storage.");
                return false; 
            }
        }

        public async Task ClearAsync()
        {
            UserName = null;
            Password = null;
            Token = null;
            PersonID = null;
            LangCode = string.Empty;
            RightMatrix = null;
            UseOriginalColors = true;
            CanHaveManyActiveTake = false;
            NetworkShareUsername = null;
            NetworkSharePassword = null;
            NetworkShareServer = null;
            //var cultureInfo = new CultureInfo("en-EN");
            //CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            //CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            await _localStorageService.RemoveItemAsync("identityData");
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
} 