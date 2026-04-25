using ControlDeAsientos.Models;
using ControlDeAsientos.Views;
using ControlDeAsientos.Services;

namespace ControlDeAsientos.Controllers;

public class SystemController
{
    private readonly ConsoleView _view;
    private readonly StudentService _studentService;
    private readonly SeatService _seatService;

    public SystemController()
    {
        _view = new ConsoleView();
        _studentService = new StudentService();
        _seatService = new SeatService();
    }

    public void Run()
    {
        bool exit = false;
        while (!exit)
        {
            _view.ShowMainMenu();
            string option = Console.ReadLine() ?? string.Empty;
            switch (option)
            {
                case "1": ManageStudents(); break;
                case "2": ManageSeats(); break;
                case "3": exit = true; _view.DisplayMessage("¡Hasta luego!"); break;
                default: _view.DisplayMessage("Opción no válida.", true); _view.WaitForKey(); break;
            }
        }
    }

    private void ManageStudents()
    {
        bool back = false;
        while (!back)
        {
            _view.ShowStudentMenu();
            string option = Console.ReadLine() ?? string.Empty;
            switch (option)
            {
                case "1": RegisterStudent(); break;
                case "2": QueryStudent(); break;
                case "3": back = true; break;
                default: _view.DisplayMessage("Opción no válida.", true); _view.WaitForKey(); break;
            }
        }
    }

    private void ManageSeats()
    {
        bool back = false;
        while (!back)
        {
            _view.ShowSeatMenu();
            string option = Console.ReadLine() ?? string.Empty;
            switch (option)
            {
                case "1": ShowMap(); break;
                case "2": AssignOrChangeSeat(); break;
                case "3": QueryAssignment(); break;
                case "4": SetupLayout(); break;
                case "5": back = true; break;
                default: _view.DisplayMessage("Opción no válida.", true); _view.WaitForKey(); break;
            }
        }
    }

    private void RegisterStudent()
    {
        var student = new Student
        {
            Name = _view.GetInput("Nombre del Estudiante"),
            Enrollment = _view.GetInput("Matrícula"),
            Major = _view.GetInput("Carrera")
        };
        _view.ShowLoading();
        try
        {
            _studentService.Create(student);
            _view.DisplayMessage("\nEstudiante registrado exitosamente.", false);
        }
        catch (Exception ex) { _view.DisplayMessage($"\n{ex.Message}", true); }
        _view.WaitForKey();
    }

    private void QueryStudent()
    {
        string enrollment = _view.GetInput("Matrícula");
        _view.ShowLoading();
        int id = _studentService.GetIdByEnrollment(enrollment);
        if (id > 0) _view.DisplayMessage($"\nEstudiante encontrado (ID: {id})", false);
        else _view.DisplayMessage("\nEstudiante no registrado.", true);
        _view.WaitForKey();
    }

    private void ShowMap()
    {
        _view.ShowLoading();
        try
        {
            var seats = _seatService.GetAll();
            _view.DisplaySeatMap(seats);
        }
        catch (Exception ex) { _view.DisplayMessage($"\n{ex.Message}", true); }
        _view.WaitForKey();
    }

    private void AssignOrChangeSeat()
    {
        try
        {
            var seats = _seatService.GetAll();
            _view.DisplaySeatMap(seats);
            
            string enrollment = _view.GetInput("Matrícula del Estudiante");
            string row = _view.GetInput("Fila deseada").ToUpper();
            if (!int.TryParse(_view.GetInput("Número deseado"), out int number))
            {
                _view.DisplayMessage("Número inválido.", true);
                _view.WaitForKey();
                return;
            }

            _view.ShowLoading();
            int studentId = _studentService.GetIdByEnrollment(enrollment);
            if (studentId == 0)
            {
                _view.DisplayMessage("\nEstudiante no encontrado.", true);
                _view.WaitForKey();
                return;
            }

            var newSeat = _seatService.GetByPosition(row, number);
            if (newSeat == null) _view.DisplayMessage("\nEl asiento no existe.", true);
            else if (newSeat.Status != "Disponible") _view.DisplayMessage("\nAsiento ocupado.", true);
            else
            {
                int oldSeatId = _seatService.GetCurrentSeatId(studentId);
                if (oldSeatId > 0) _seatService.UpdateAssignment(studentId, oldSeatId, newSeat.Id);
                else _seatService.Assign(studentId, newSeat.Id);
                _view.DisplayMessage("\nProceso completado exitosamente.", false);
            }
        }
        catch (Exception ex) { _view.DisplayMessage($"\n{ex.Message}", true); }
        _view.WaitForKey();
    }

    private void QueryAssignment()
    {
        string enrollment = _view.GetInput("Matrícula del Estudiante");
        _view.ShowLoading();
        try
        {
            var info = _seatService.GetAssignmentInfo(enrollment);
            if (info != null)
            {
                _view.DisplayMessage($"\nEstudiante: {info.Name}");
                _view.DisplayMessage($"Asiento: Fila {info.Row}, Número {info.Number}");
            }
            else _view.DisplayMessage("\nNo se encontró asignación.", true);
        }
        catch (Exception ex) { _view.DisplayMessage($"\n{ex.Message}", true); }
        _view.WaitForKey();
    }

    private void SetupLayout()
    {
        _view.DisplayMessage("ADVERTENCIA: Se borrarán todas las asignaciones.");
        if (!int.TryParse(_view.GetInput("Filas"), out int rows) || !int.TryParse(_view.GetInput("Columnas"), out int cols))
        {
            _view.DisplayMessage("Valores inválidos.", true);
            _view.WaitForKey();
            return;
        }
        _view.ShowLoading();
        try
        {
            _seatService.InitializeLayout(rows, cols);
            _view.DisplayMessage("\nMatriz creada exitosamente.", false);
        }
        catch (Exception ex) { _view.DisplayMessage($"\n{ex.Message}", true); }
        _view.WaitForKey();
    }
}
