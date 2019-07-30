using System;
using System.Runtime.InteropServices;

namespace crm.shared.Services
{
    public abstract class ServiceBase
    {
        public ServiceBase()
        {
            // ALPN is not available on macOS so only use HTTP/2 without TLS
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            }
        }
    }
}