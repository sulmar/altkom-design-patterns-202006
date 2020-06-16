using System;

namespace SingletonPattern
{
    // Lazy generic singleton
    public sealed class LazySingleton<T>
        where T : new()
    {
        private static readonly Lazy<T> lazy = new Lazy<T>(() => new T());

        private LazySingleton() { }

        public static T Instance => lazy.Value;
    }
}
