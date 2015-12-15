[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DeadPool.Mvc4.App_Start.DeadPoolConfig), "PreStart")]

namespace DeadPool.Mvc4.App_Start
{
    public static class DeadPoolConfig
    {
        public static void PreStart()
        {
            Register();
        }

        public static void Register()
        {

        }

    }
}