﻿using FluentScheduler;

namespace Sentinel
{
    public class SentinelInitializer
    {
        private readonly Registry _service;

        internal SentinelInitializer(Registry service)
        {
            _service = service;
        }

        public void Start()
        {
            TaskManager.Initialize(_service);
        }
    }
}