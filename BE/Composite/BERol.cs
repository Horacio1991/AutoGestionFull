namespace BE.BEComposite
{
    // Rol compuesto, puede contener otros roles o permisos.
    public class BERol : BEComponente
    {
        private List<BEComponente> _hijos = new List<BEComponente>();
        public override List<BEComponente> Hijos => _hijos.ToList();
        public override void AgregarHijo(BEComponente c) => _hijos.Add(c);
        public override void EliminarHijo(BEComponente c) => _hijos.RemoveAll(x => x.Id == c.Id);
        public override void VaciarHijos() => _hijos.Clear();
    }
}
