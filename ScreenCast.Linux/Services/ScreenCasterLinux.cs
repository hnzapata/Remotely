﻿using Microsoft.Extensions.DependencyInjection;
using Remotely.ScreenCast.Core;
using Remotely.ScreenCast.Core.Interfaces;
using Remotely.ScreenCast.Core.Services;
using Remotely.Shared.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace Remotely.ScreenCast.Linux.Services
{
    public class ScreenCasterLinux : ScreenCasterBase, IScreenCaster
    {
        public async Task BeginScreenCasting(ScreenCastRequest screenCastRequest)
        {
            try
            {
                var conductor = ServiceContainer.Instance.GetRequiredService<Conductor>();
                await conductor.CasterSocket.SendCursorChange(new CursorInfo(null, Point.Empty, "default"), new List<string>() { screenCastRequest.ViewerID });
                _ = BeginScreenCasting(screenCastRequest.ViewerID, screenCastRequest.RequesterName, ServiceContainer.Instance.GetRequiredService<IScreenCapturer>());
            }
            catch (Exception ex)
            {
                Logger.Write(ex);
            }
        }
    }
}