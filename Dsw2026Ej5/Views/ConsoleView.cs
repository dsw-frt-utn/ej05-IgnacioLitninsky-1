using Dsw2026Ej5.Data;
using Dsw2026Ej5.Domain;
using System.Linq;

namespace Dsw2026Ej5.Views;

public class ConsoleView
{
    private static List<VehiculoViewModel> _vehiculos = Controlador.GetVehiculos();
    public static void DibujarMenu()
    {
        string? opcion = null;
        do
        {
            LimpiarPantalla();
            DibujarLinea();
            CentrarTexto("Menú Principal - Empresa de Transporte", out int _);
            DibujarLinea();
            Console.WriteLine("Elija una opción: \n");
            Console.WriteLine("1. Listar vehículos");
            Console.WriteLine("2. Agregar vehículo");
            Console.WriteLine("3. Salir");
            Console.WriteLine("\n");
            Console.WriteLine("Ingrese su opción: ");
            opcion = Console.ReadLine();
            if (opcion == "1")
            {
                Console.WriteLine("Listando vehículos...");
                ListarVehiculos();
            }
            else if (opcion == "2")
            {
                AgregarVehiculo();
            }
        }
        while (opcion != "3");
    }
    public static void CentrarTexto(string? texto, out int usado, int? ancho = null, bool salto = true)
    {
        texto ??= string.Empty;
        ancho ??= Console.WindowWidth;
        int largo = texto.Length;
        if (largo > ancho)
        {
            largo = ancho.Value;
            texto = texto.Substring(0, ancho.Value);
        }
        int espacios = (ancho.Value - largo) / 2;
        espacios = espacios % 2 == 0 ? espacios : espacios + 1;
        string fin = salto ? "\n" : string.Empty;
        string final = new string(' ', espacios) + texto + fin;
        Console.Write(final);
        usado = final.Length;
    }
    public static void LimpiarPantalla()
    {
        Console.Clear();
    }

    public static void DibujarLinea()
    {
        var with = Console.WindowWidth;
        for (int i = 0; i < with; i++)
        {
            Console.Write("-");
        }
    }

    private static void ListarVehiculos()
    {
        LimpiarPantalla();
        string[] columnas = { "Patente", "Vehículo", "Tipo", "Cap. Carga", "Km/l", "Año", "L.Extra", "Kms a recorrer" };
        DibujarEncabezado(columnas);
        DibjuarDatos(columnas.Length);
        DibujarLinea();
        Console.Write("\n");
        Console.Write("\n");
        Console.WriteLine("Presione una tecla para calcular el total de consumos...");
        Console.ReadLine();
        Dictionary<string, double> vehiculos = new Dictionary<string, double>();
        foreach (VehiculoViewModel vehiculo in _vehiculos)
        {
            vehiculos.Add(vehiculo.GetPatente(), vehiculo.GetKmARecorrer());
        }
        (double, double) totalConsumos = Controlador.CalcularConsumos(vehiculos);               
            DibujarLinea();
        Console.WriteLine($"Total consumo Vehículos Eléctricos: {totalConsumos.Item1:F2} kWh");
        Console.WriteLine($"Total consumo Vehículos Combustible: {totalConsumos.Item2:F2} Litros");
        DibujarLinea();
        Console.Write("\n");
        Console.Write("\n");
        Console.WriteLine("Presione una tecla para salir...");
        Console.ReadLine();
    }
    private static void DibujarEncabezado(params string[] columnas)
    {
        DibujarLinea();
        int ancho = Console.WindowWidth / columnas.Length;

        foreach (var columna in columnas)
        {
            Console.Write("|");
            CentrarTexto(columna, out int l, ancho - 1, false);
            Console.Write("".PadRight(ancho - 1 - l));
        }
        Console.Write("\n");
        DibujarLinea();
    }
    private static void DibjuarDatos(int columnas)
    {
        int ancho = Console.WindowWidth / columnas;
        foreach (var vehiculo in _vehiculos)
        {
            Console.Write("|");
            CentrarTexto(vehiculo.GetPatente(), out int l, ancho - 1, false);
            Console.Write("".PadRight(ancho - 1 - l));
            Console.Write("|");
            CentrarTexto(vehiculo.GetVehiculo(), out l, ancho - 1, false);
            Console.Write("".PadRight(ancho - 1 - l));
            Console.Write("|");
            CentrarTexto(vehiculo.GetTipo(), out l, ancho - 1, false);
            Console.Write("".PadRight(ancho - 1 - l));
            Console.Write("|");
            CentrarTexto(vehiculo.GetCapacidadCarga().ToString(), out l, ancho - 1, false);
            Console.Write("".PadRight(ancho - 1 - l));
            Console.Write("|");
            CentrarTexto(vehiculo.GetKmPorLitro().ToString(), out l, ancho - 1, false);
            Console.Write("".PadRight(ancho - 1 - l));
            Console.Write("|");
            CentrarTexto(vehiculo.GetAnio().ToString(), out l, ancho - 1, false);
            Console.Write("".PadRight(ancho - 1 - l));
            Console.Write("|");
            CentrarTexto(vehiculo.GetLitrosExtra().ToString(), out l, ancho - 1, false);
            Console.Write("".PadRight(ancho - 1 - l));
            Console.Write("|");
            CentrarTexto(vehiculo.GetKmARecorrer().ToString(), out l, ancho - 1, false);
            Console.Write("".PadRight(ancho - 1 - l));
        }

    }

    private static void AgregarVehiculo()
    {
        LimpiarPantalla();

        DibujarLinea();
        CentrarTexto("Agregar Vehiculo", out int _);
        DibujarLinea();
        Console.WriteLine();

        
        Console.WriteLine("Seleccione tipo:");
        Console.WriteLine("1. Eléctrico");
        Console.WriteLine("2. Combustible");
        Console.Write("Opción: ");
        string tipo = Console.ReadLine()?.Trim() ?? "0";

        while (tipo != "1" && tipo != "2")
        {
            Console.Write("Opción inválida. Ingrese 1 o 2: ");
            tipo = Console.ReadLine()?.Trim() ?? "0";
        }

        Console.WriteLine();

       
        Console.Write("Patente: ");
        string patente = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Marca: ");
        string marca = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Modelo: ");
        string modelo = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Año: ");
        int anio;
        while (!int.TryParse(Console.ReadLine(), out anio))
        {
            Console.Write("Valor inválido. Ingrese año: ");
        }

        Console.Write("Capacidad de carga: ");
        double carga;
        while (!double.TryParse(Console.ReadLine(), out carga))
        {
            Console.Write("Valor inválido. Ingrese capacidad: ");
        }

        Console.WriteLine();

        
        Console.WriteLine("Seleccione sucursal:");
        Console.WriteLine("1. SUC01");
        Console.WriteLine("2. SUC02");
        Console.Write("Opción: ");

        string opSucursal = Console.ReadLine()?.Trim() ?? "1";

        while (opSucursal != "1" && opSucursal != "2")
        {
            Console.Write("Opción inválida. Ingrese 1 o 2: ");
            opSucursal = Console.ReadLine()?.Trim() ?? "1";
        }

        Sucursal sucursal;

        if (opSucursal == "1")
            sucursal = Persistencia.GetSucursales()[0];
        else
            sucursal = Persistencia.GetSucursales()[1];

        Console.WriteLine();

        Vehiculo nuevo;

     
        if (tipo == "1")
        {
            Console.Write("kWh base: ");
            double kwh;

            while (!double.TryParse(Console.ReadLine(), out kwh))
            {
                Console.Write("Valor inválido. Ingrese kWh: ");
            }

            nuevo = new VehiculoElectrico(
                patente,
                marca,
                modelo,
                anio,
                carga,
                sucursal,
                kwh
            );
        }
        else
        {
            Console.Write("Km por litro: ");
            double kmPorLitro;

            while (!double.TryParse(Console.ReadLine(), out kmPorLitro))
            {
                Console.Write("Valor inválido. Ingrese Km/l: ");
            }

            Console.Write("Litros extra: ");
            double litrosExtra;

            while (!double.TryParse(Console.ReadLine(), out litrosExtra))
            {
                Console.Write("Valor inválido. Ingrese litros extra: ");
            }

            nuevo = new VehiculoCombustible(
                patente,
                marca,
                modelo,
                anio,
                carga,
                sucursal,
                kmPorLitro,
                litrosExtra
            );
        }

        Persistencia.AgregarVehiculo(nuevo);
        _vehiculos = Controlador.GetVehiculos();

        Console.WriteLine();
        Console.WriteLine("Vehículo agregado correctamente.");
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
    }



    /*  private static void AgregarVehiculo()
      {
          LimpiarPantalla();

          Console.Write("Patente: ");
          string patente = Console.ReadLine();

          Console.Write("Marca: ");
          string marca = Console.ReadLine();

          Console.Write("Modelo: ");
          string modelo = Console.ReadLine();

          Console.Write("Año: ");
          int anio = int.Parse(Console.ReadLine());

          Console.Write("Capacidad de carga: ");
          double carga = double.Parse(Console.ReadLine());

          Console.WriteLine("Tipo:");
          Console.WriteLine("1. Electrico");
          Console.WriteLine("2. Combustible");
          string tipo = Console.ReadLine();

          Vehiculo nuevo;

      if (tipo == "1")
      {
          nuevo = new VehiculoElectrico(
              patente,
              marca,
              modelo,
              anio,
              carga,
              Persistencia.GetSucursales()[0],
              16
          );
      }
      else
      {
          nuevo = new VehiculoCombustible(
              patente,
              marca,
              modelo,
              anio,
              carga,
              Persistencia.GetSucursales()[0],
              8,
              1.5
          );
      }

          Persistencia.AgregarVehiculo(nuevo);

          Console.WriteLine();
          Console.WriteLine("Vehículo agregado correctamente.");
          Console.WriteLine("Presione Enter para continuar...");
          Console.ReadLine();
  }*/



}
