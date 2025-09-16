using BlazorTool.Client.Models;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using static BlazorTool.Client.Pages.SchedulerPage;
namespace BlazorTool.Client.Services

{
    public class AppointmentService
    {
        private List<SchedulerAppointment> _appointments = new();
        private readonly ApiServiceClient _apiServiceClient;

        public AppointmentService(ApiServiceClient apiServiceClient)
        {
            _apiServiceClient = apiServiceClient;
        }
        
        public async Task InitializeAsync()
        {
            var orders = await _apiServiceClient.GetWorkOrdersCachedAsync();
            _appointments = ConvertAppointmentsFromOrders(orders);
        }

        public void ClearAppointments()
        {
            _appointments.Clear();

        }


        public async Task<List<SchedulerAppointment>> GetAllAppointments()
        {
            if (_appointments == null || !_appointments.Any())
            {
                var orders = await _apiServiceClient.GetWorkOrdersCachedAsync();
                _appointments = GetAppointmentsFromOrders(orders);
            }
            return _appointments;
        }

        public async Task<List<SchedulerAppointment>> GetTakenAppointments(List<WorkOrder>? orders = null)
        {
                //if (_appointments == null || !_appointments.Any())
                {
                    if (orders == null)
                    {
                        Console.WriteLine("===> _appointments = null, run _apiServiceClient.GetWorkOrdersCachedAsync()");
                        orders = await _apiServiceClient.GetWorkOrdersCachedAsync();
                    }
                    _appointments = GetAppointmentsFromOrders(orders);
                }
                //return _appointments.Where(x => !string.IsNullOrWhiteSpace(x.DepName) && x.Start != null).ToList();
                return _appointments.Where(x => x.Start != null).ToList();
        }

        public SchedulerAppointment? GetAppointmentById(int id)
        {
            return _appointments.FirstOrDefault(x => x.AppointmentId == id);
        }

        public async Task AddAppointment(SchedulerAppointment appointment)
        {
            //na razie nie dodajemy nowych zadań, tylko aktualizujemy istniejące

            //appointment.AppointmentId = 0;
            //_appointments.Add(appointment);
            //await _apiServiceClient.SaveWorkOrderAsync((WorkOrder)appointment);
        }

        public async Task<SchedulerAppointment> RefreshAppointmentById(int appId)
        {
            var newWo = await _apiServiceClient.GetWorkOrderByIdCachedAsync(appId);
            _appointments.RemoveAll(x => x.AppointmentId == appId);
            if (newWo != null)
            {
                _appointments.Add(new SchedulerAppointment(newWo));
                return new SchedulerAppointment(newWo);
            }
            return new SchedulerAppointment();
        }

        public async Task<SingleResponse<WorkOrder>> UpdateAppointment(SchedulerAppointment appointment)
        {
            var existingAppointment = _appointments.FirstOrDefault(x => x.AppointmentId == appointment.AppointmentId);
            if (appointment.AssignedPerson == Person.NotAssigned.Name)
                    appointment.AssignedPerson = string.Empty; 

            if (existingAppointment != null)
            {
                // Update existing appointment
                if (!Object.ReferenceEquals(existingAppointment, appointment))
                {
                    existingAppointment = appointment.ShallowCopy();
                }

            } 

            var workOrderToUpdate = (WorkOrder)appointment;
            var updateResult = await _apiServiceClient.UpdateWorkOrderAsync(workOrderToUpdate);

            if (!updateResult.IsValid)
            {
                Console.WriteLine($"Error updating appointment: {string.Join(", ", updateResult.Errors)}");
            }
            return updateResult;
        }

        public async Task<SingleResponse<WorkOrder>> SaveNewAppointment(SchedulerAppointment appointment)
        {
            if (appointment == null)
            {
                return new SingleResponse<WorkOrder>
                {
                    IsValid = false,
                    Errors = new List<string> { "Appointment is null." }
                };
            }

            var workOrderToCreate = (WorkOrder)appointment;
            var saveResult = await _apiServiceClient.SaveWorkOrderAsync(workOrderToCreate);
            if (saveResult.IsValid && saveResult.Data != null)
            {
                _appointments.Add(new SchedulerAppointment(saveResult.Data));
            }
            else
            {
                Console.WriteLine($"Error: Failed to save the new appointment. Errors: {string.Join(", ", saveResult.Errors)}");
            }
            return saveResult;
        }

        public async Task<SingleResponse<WorkOrder>> RemoveAppointment(SchedulerAppointment ap)
        {
            if (ap != null)
            {
                //ONLY remove StartDate for removing from scheduler
                ap.StartDate = null;
                var tryClose = await _apiServiceClient.UpdateWorkOrderAsync((WorkOrder)ap);
                if (!tryClose.IsValid)
                {
                    Console.WriteLine($"Error closing appointment: {string.Join(", ", tryClose.Errors)}");
                    return new SingleResponse<WorkOrder>
                    {
                        IsValid = false,
                        Errors = tryClose.Errors
                    };
                }
                else
                {
                    _appointments.Remove(ap);
                    return new SingleResponse<WorkOrder>
                    {
                        IsValid = true,
                        Data = tryClose.Data,
                        Errors = new List<string>()
                    };
                }
            }
            return new SingleResponse<WorkOrder>
            {
                IsValid = false,
                Errors = new List<string> { "Appointment is null." }
            };
        }

        public async Task<SingleResponse<WorkOrder?>> CloseAppointment(SchedulerAppointment ap)
        {
            if (ap != null)
            {
                var tryClose = await _apiServiceClient.CloseWorkOrderAsync((WorkOrder)ap);
                var updWorkOrder = await _apiServiceClient.GetWorkOrderByIdAsync(ap.WorkOrderID);
                if (!tryClose.IsValid)
                {
                    Console.WriteLine($"Error closing appointment: {string.Join(", ", tryClose.Errors)}");
                }
                else
                {
                   // _appointments.Remove(ap);                  
                }
                return new SingleResponse<WorkOrder?>
                {
                    IsValid = tryClose.IsValid,
                    Data = updWorkOrder,
                    Errors = tryClose.Errors
                };
            }
            return new SingleResponse<WorkOrder?>
            {
                IsValid = false,
                Errors = new List<string> { "Appointment is null." }
            };
        }

        public List<SchedulerAppointment> GetAppointmentsFromOrders(IEnumerable<WorkOrder> orders)
        {
            if (orders == null || !orders.Any())
                return new List<SchedulerAppointment>();
            return ConvertAppointmentsFromOrders(orders.ToList());
        }

        private List<SchedulerAppointment> ConvertAppointmentsFromOrders(List<WorkOrder> orders)
        {
            var appointments = new List<SchedulerAppointment>();
            foreach (var order in orders)
                 appointments.Add(new SchedulerAppointment(order));

            return appointments;
        }

        public async Task<WorkOrderInfo?> GetWorkOrderInfo(SchedulerAppointment appointment)
        {
            if (appointment == null)
            {
                return null;
            }
            var workOrderInfo = await _apiServiceClient.GetWOInfo(appointment.WorkOrderID);
            return workOrderInfo;
        }
    }
}
