using VenterinaryClinicSystem.Repository;
using VenterinaryClinicSystem.Services;
using VenterinaryClinicSystem.UI;

// Inicialización con Inyección de Dependencias (Dependency Injection):

// 1. Instanciación de los repositorios puros (Bóvedas de almacenamiento)
IClienteRepository clienteRepository = new ClienteRepository();
IMascotaRepository mascotaRepository = new MascotaRepository();

// 2. Inyección de IClienteRepository e IMascotaRepository en el Servicio de Negocio
IPatientService patientService = new PatientService(clienteRepository, mascotaRepository);

// 3. Inyección del Servicio de Negocio en la Capa de Presentación (UI)
ConsoleUI consoleUI = new ConsoleUI(patientService);

// 4. Iniciar la aplicación
await consoleUI.IniciarAsync();