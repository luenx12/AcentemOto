using System;
using System.Threading.Tasks;

namespace AcentemOto.Services
{
    public enum GonderimHizi { Hizli, Orta, Yavas }

    public class AntiSpamEngine
    {
        private readonly Random _random = new Random();
        private const int MESSAGES_BEFORE_COOLDOWN = 20;
        
        public GonderimHizi SeciliHiz { get; set; } = GonderimHizi.Orta;

        public async Task WaitBetweenMessagesAsync()
        {
            int delaySeconds;
            switch(SeciliHiz)
            {
                case GonderimHizi.Hizli:
                    delaySeconds = _random.Next(5, 11);
                    break;
                case GonderimHizi.Yavas:
                    delaySeconds = _random.Next(25, 41);
                    break;
                case GonderimHizi.Orta:
                default:
                    // 12 ile 28 saniye arası rastgele bekleme
                    delaySeconds = _random.Next(12, 29);
                    break;
            }
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
