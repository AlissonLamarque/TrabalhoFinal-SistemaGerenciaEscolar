namespace PF_GerenciaEscolar.Data.Enum
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TurmaRouteAttribute : Attribute
    {
        public string Route { get; }

        public TurmaRouteAttribute(string route)
        {
            Route = route;
        }
    }
}
