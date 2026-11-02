
using System;

namespace Biblioteca_Hispana
{
    public class Prestamo
    {
        public Usuario Usuario { get; }
        public Material Material { get; }

        public DateTime FechaPrestamo { get; }
        public DateTime FechaVencimiento { get; }
        public DateTime? FechaDevolucion { get; private set; }

        public bool Devuelto => FechaDevolucion.HasValue;

        public int DiasRetraso { get; private set; }
        public decimal Multa { get; private set; }

        public Prestamo(Usuario usuario, Material material)
        {
            Usuario = usuario;
            Material = material;

            FechaPrestamo = DateTime.Today;
            FechaVencimiento = FechaPrestamo.AddDays(
                material.DiasDePrestamo
            );

            FechaDevolucion = null;
            DiasRetraso = 0;
            Multa = 0;
        }

        public void RegistrarDevolucion(DateTime fechaDevolucion)
        {
            if (Devuelto)
            {
                return;
            }

            if (fechaDevolucion.Date < FechaPrestamo.Date)
            {
                return;
            }

            FechaDevolucion = fechaDevolucion.Date;

            DiasRetraso = Math.Max(
                0,
                (FechaDevolucion.Value.Date -
                 FechaVencimiento.Date).Days
            );

            Multa = Material.CalcularMulta(DiasRetraso);
        }

        public string MostrarInformacion()
        {
            string estado = Devuelto ? "Devuelto" : "Activo";

            return
                $"Usuario: {Usuario.Nombre}\n" +
                $"Material: {Material.Titulo}\n" +
                $"Fecha de préstamo: {FechaPrestamo:dd/MM/yyyy}\n" +
                $"Fecha límite: {FechaVencimiento:dd/MM/yyyy}\n" +
                $"Estado: {estado}\n" +
                $"Días de retraso: {DiasRetraso}\n" +
                $"Importe para cobro: ${Multa:F2}";
        }
    }
}
