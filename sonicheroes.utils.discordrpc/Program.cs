﻿using System;
using Heroes.SDK;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using Reloaded.Mod.Interfaces.Internal;

namespace SonicHeroes.Utils.DiscordRPC
{
    public class Program : IMod
    {
        private IModLoader _modLoader;
        private WeakReference<IReloadedHooks> _reloadedHooks;
        private HeroesRPC _heroesRpc;

        public void Start(IModLoaderV1 loader)
        {
            _modLoader = (IModLoader)loader;
            _reloadedHooks = _modLoader.GetController<IReloadedHooks>();

            /* Your mod code starts here. */
            if (_reloadedHooks.TryGetTarget(out var hooks))
            {
                SDK.Init(hooks);
                _heroesRpc = new HeroesRPC();
            }
        }

        /* Mod loader actions. */
        public void Suspend() => _heroesRpc.Suspend();
        public void Resume() => _heroesRpc.Resume();

        public void Unload()
        {
            Suspend();
            _heroesRpc.Dispose();
        }

        public bool CanUnload()  => true;
        public bool CanSuspend() => true;

        /* Automatically called by the mod loader when the mod is about to be unloaded. */
        public Action Disposing { get; }
    }
}
