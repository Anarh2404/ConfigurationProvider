using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace ConfigurationProvider.MyConfiguration
{
    public class MyConfigurationProvider : Microsoft.Extensions.Configuration.ConfigurationProvider, IDisposable
    {
        private readonly IDisposable _changeTokenRegistration;
        private CancellationChangeToken _cancellationChangeToken;
        private CancellationTokenSource _cancellationTokenSource;

        public MyConfigurationProvider()
        {
            _changeTokenRegistration = ChangeToken.OnChange(() => CreateChangeToken(), () => Load());
            _ = RunChange();
        }

        private static int _count;
        public override void Load()
        {
            this.Data = new Dictionary<string, string>
            {
                ["Auth:ServiceUrl"] = "localhost:" + _count.ToString("0000")
            };
            _count++;
            OnReload();
        }

        private IChangeToken CreateChangeToken()
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            return new CancellationChangeToken(_cancellationTokenSource.Token);
        }

        private async Task RunChange()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromSeconds(2));
                _cancellationTokenSource.Cancel();
            }
        }
        
        public void Dispose()
        {
            _changeTokenRegistration?.Dispose();
        }
    }
}