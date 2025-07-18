namespace BE.BEComposite
{
    // Clase base para los componentes del sistema.
    public abstract class BEComponente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public abstract List<BEComponente> Hijos { get; }
        public abstract void AgregarHijo(BEComponente c);
        public abstract void VaciarHijos();
        public abstract void EliminarHijo(BEComponente c);
    }
}
