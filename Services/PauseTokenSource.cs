using System.Threading;
using System.Threading.Tasks;

namespace ImapMigrator.Services
{
    public class PauseTokenSource
    {
        private readonly ManualResetEventSlim _mre = new ManualResetEventSlim(true);

        public bool IsPaused => !_mre.IsSet;

        public void Pause()
        {
            _mre.Reset();
        }

        public void Resume()
        {
            _mre.Set();
        }

        public async Task WaitWhilePausedAsync(CancellationToken token = default)
        {
            if (_mre.IsSet) return;

            await Task.Run(() =>
            {
                _mre.Wait(token);
            }, token).ConfigureAwait(false);
        }

        public PauseToken Token => new PauseToken(this);
    }

    public readonly struct PauseToken
    {
        private readonly PauseTokenSource _source;

        public PauseToken(PauseTokenSource source)
        {
            _source = source;
        }

        public bool IsPaused => _source?.IsPaused ?? false;

        public Task WaitWhilePausedAsync(CancellationToken token = default)
        {
            return _source?.WaitWhilePausedAsync(token) ?? Task.CompletedTask;
        }
    }
}
