using Zenject;

namespace Jelewow.DNK.Extensions
{
    public static class ZenjectExtensions
    {
        public static void BindObjectOnScene<T>(this DiContainer container, T objectOnScene) where T : UnityEngine.Object
        {
            container.BindInstance(objectOnScene).AsSingle();
        }
        
        public static void BindScenario<T>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle();
        }
        
        public static void BindService<T>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle();
        }
        
        public static void BindTask<T>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle();
        }
        
        public static void BindHandler<T>(this DiContainer container)
        {
            container.BindInterfacesAndSelfTo<T>().AsSingle();
        }
        
        public static void BindScriptableObject<T>(this DiContainer container, T scriptableObject) where T : UnityEngine.ScriptableObject
        {
            container.BindInstance(scriptableObject).AsSingle();
        }
        
        public static void BindViaSpawnNewPrefab<T>(this DiContainer container, T prefab) where T : UnityEngine.Behaviour
        {
            container.BindInterfacesAndSelfTo<T>().FromComponentsInNewPrefab(prefab).AsSingle();
        }
        
        public static void BindPrefab<T>(this DiContainer container, T prefab) where T : UnityEngine.MonoBehaviour
        {
            container.BindInstance(prefab).AsSingle();
        }
        
        public static ScreenBindingChain<TScreenView> BindScreenView<TScreenView>(this DiContainer container, TScreenView prefab)
        {
            var chain = new ScreenBindingChain<TScreenView>();
            chain.BindScreenView(container, prefab);
            return chain;
        }

        public sealed class ScreenBindingChain<TScreenView>
        {
            private DiContainer _container;
            
            public void BindScreenView(DiContainer container, TScreenView prefab)
            {
                _container = container;
                container.BindInstance(prefab).AsSingle();
            }
            
            public void WithScreenController<TScreenController>()
            {
                _container.BindInterfacesAndSelfTo<TScreenController>().AsSingle();
            }
        }
    }
}