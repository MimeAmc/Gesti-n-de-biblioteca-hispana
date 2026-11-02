
using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblioteca_Hispana
{
    public class Usuario
    {
        private readonly List<Prestamo> prestamos =
            new List<Prestamo>();

        public string Nombre { get; }
        public string NumeroSocio { get; }

        public int CantidadPrestados =>
            prestamos.Count(p => !p.Devuelto);

        public Usuario(string nombre, string numeroSocio)
        {
            Nombre = nombre;
            NumeroSocio = numeroSocio;
        }

        public bool PuedeSolicitarPrestamo()
        {
            return CantidadPrestados < 3;
        }

        public bool TienePrestado(Material material)
        {
            return prestamos.Any(p =>
                !p.Devuelto && p.Material == material);
        }

        public void RegistrarPrestamo(Prestamo prestamo)
        {
            if (prestamo != null &&
                PuedeSolicitarPrestamo() &&
                !TienePrestado(prestamo.Material))
            {
                prestamos.Add(prestamo);
            }
        }

        public Prestamo? BuscarPrestamoActivo(string codigo)
        {
            return prestamos.FirstOrDefault(p =>
                !p.Devuelto &&
                p.Material.Codigo.Equals(
                    codigo,
                    StringComparison.OrdinalIgnoreCase));
        }

        public bool RegistrarDevolucion(
            string codigo,
            DateTime fechaDevolucion)
        {
            Prestamo? prestamo = BuscarPrestamoActivo(codigo);

            if (prestamo == null)
            {
                return false;
            }

            prestamo.RegistrarDevolucion(fechaDevolucion);

            return prestamo.Devuelto;
        }

        public List<Prestamo> ObtenerPrestamos()
        {
            return prestamos.ToList();
        }

        public void MostrarPrestamos()
        {
            Console.WriteLine(
                $"\nPréstamos de {Nombre} ({NumeroSocio})");

            if (prestamos.Count == 0)
            {
                Console.WriteLine("No hay préstamos registrados.");
                return;
            }

            foreach (Prestamo prestamo in prestamos)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine(prestamo.MostrarInformacion());
            }
        }
    }
}
