using ControlDeAsientos.Models;

namespace ControlDeAsientos.Views;

public class ConsoleView
{
    public void ShowMainMenu()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("   SISTEMA DE CONTROL DE ASIENTOS");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Gestionar Estudiantes");
        Console.WriteLine("2. Gestionar Asientos y Asignaciones");
        Console.WriteLine("3. Salir");
        Console.WriteLine("========================================");
        Console.Write("Seleccione una opción: ");
    }

    public void ShowStudentMenu()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("   GESTIÓN DE ESTUDIANTES");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Registrar Estudiante");
        Console.WriteLine("2. Consultar Estudiante");
        Console.WriteLine("3. Volver al Menú Principal");
        Console.WriteLine("========================================");
        Console.Write("Seleccione una opción: ");
    }

    public void ShowSeatMenu()
    {
        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("   GESTIÓN DE ASIENTOS");
        Console.WriteLine("========================================");
        Console.WriteLine("1. Ver Mapa de Asientos");
        Console.WriteLine("2. Asignar o Cambiar Asiento");
        Console.WriteLine("3. Consultar Asignación Actual");
        Console.WriteLine("4. Configurar Nueva Matriz (Reset)");
        Console.WriteLine("5. Volver al Menú Principal");
        Console.WriteLine("========================================");
        Console.Write("Seleccione una opción: ");
    }

    public string GetInput(string label)
    {
        Console.Write($"{label}: ");
        return Console.ReadLine() ?? string.Empty;
    }

    public void DisplayMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void DisplayMessage(string message, bool isError)
    {
        if (isError)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR]: {message}");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[OK]: {message}");
        }
        Console.ResetColor();
    }

    public void ShowLoading()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("\nCargando...");
        Console.ResetColor();
    }

    public void WaitForKey()
    {
        Console.WriteLine("\nPresione cualquier tecla para continuar...");
        Console.ReadKey();
    }

    public void DisplaySeatMap(List<Seat> seats)
    {
        Console.WriteLine("\n--- DISTRIBUCIÓN ACTUAL DE ASIENTOS ---");
        var rows = seats.GroupBy(s => s.Row).OrderBy(g => g.Key);
        foreach (var rowGroup in rows)
        {
            Console.Write($"Fila {rowGroup.Key}: ");
            foreach (var seat in rowGroup.OrderBy(s => s.Number))
            {
                string statusIcon = seat.Status == "Disponible" ? "[ ]" : "[X]";
                Console.Write($"{statusIcon}{seat.Number}  ");
            }
            Console.WriteLine();
        }
        Console.WriteLine("----------------------------------------");
    }
}
