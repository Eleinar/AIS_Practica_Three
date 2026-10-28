using Ninject;

namespace BusinessLogic
{
    /// <summary>
    /// Вспомогательная фабрика поверх IoC-контейнера Ninject.
    /// Предпочтительно в View создавать Logic через StandardKernel напрямую.
    /// </summary>
    public static class LogicFactory
    {
        private static readonly Lazy<IKernel> Kernel =
            new(() => new StandardKernel(new SimpleConfigModule()));

        /// <summary>
        /// Возвращает Logic с внедрёнными репозиториями (Singleton) через Ninject
        /// </summary>
        /// <returns>Экземпляр бизнес-логики</returns>
        public static Logic Create() => Kernel.Value.Get<Logic>();

        /// <summary>
        /// Создаёт новый kernel с модулем конфигурации (для слоя View)
        /// </summary>
        /// <returns>Настроенный IoC-контейнер</returns>
        public static IKernel CreateKernel() => new StandardKernel(new SimpleConfigModule());
    }
}
