using System;
using System.Threading.Tasks;

namespace AcentemOto.Services
{
    public class AntiSpamEngine
    {
        private readonly Random _random = new Random();
        private const int MESSAGES_BEFORE_COOLDOWN = 20;

        public async Task WaitBetweenMessagesAsync()
        {
            // 12 ile 28 saniye arası rastgele bekleme
            int delaySeconds = _random.Next(12, 29);
            await Task.Delay(TimeSpan.FromSeconds(delaySeconds));
        }

        public async Task CooldownIfNecessaryAsync(int currentMessageCount)
        {
            if (currentMessageCount > 0 && currentMessageCount % MESSAGES_BEFORE_COOLDOWN == 0)
            {
                // 3 ile 5 dakika arası rastgele uzun mola (180 - 300 saniye)
                int cooldownSeconds = _random.Next(180, 301);
                await Task.Delay(TimeSpan.FromSeconds(cooldownSeconds));
            }
        }
    }
}
