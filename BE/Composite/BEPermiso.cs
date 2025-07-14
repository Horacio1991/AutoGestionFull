namespace BE.BEComposite
{
    public class BEPermiso : BEComponente
    {
        public override List<BEComponente> Hijos => new List<BEComponente>();
        public override void AgregarHijo(BEComponente c) => throw new NotImplementedException();
        public override void EliminarHijo(BEComponente c) => throw new NotImplementedException();
        public override void VaciarHijos() => throw new NotImplementedException();
        public override string ToString() => Nombre;
    }
}
