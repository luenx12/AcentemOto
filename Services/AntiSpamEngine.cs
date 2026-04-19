using System;
using System.Threading;
using System.Threading.Tasks;

namespace AcentemOto.Services
{
    public enum GonderimHizi { Hizli, Orta, Yavas }

    public class AntiSpamEngine
    {
        private readonly Random _random = new Random();
        private const int MESSAGES_BEFORE_COOLDOWN = 20;
        private const int MAX_PER_HOUR = 45;   // Saatte maksimum mesaj
        private const int MAX_PER_DAY  = 250;  // Günde maksimum mesaj

        private int _hourlyCount = 0;
        private int _dailyCount  = 0;
        private DateTime _hourStart = DateTime.Now;
        private DateTime _dayStart  = DateTime.Now.Date;

        public GonderimHizi SeciliHiz { get; set; } = GonderimHizi.Orta;

        /// <summary>
        /// Saatlik/günlük limit kontrolü yapar; gerekirse bekler veya false döner (durdur sinyali).
        /// Her başarılı gönderimden ÖNCE çağrılmalıdır.
        /// </summary>
        public async Task<bool> CheckAndWaitForLimitsAsync(IProgress<string> progress, CancellationToken ct)
        {
            // Gün sıfırlama
            if (DateTime.Now.Date > _dayStart)
            {
                _dayStart    = DateTime.Now.Date;
                _dailyCount  = 0;
                _hourlyCount = 0;
                _hourStart   = DateTime.Now;
            }

            // Saat sıfırlama
            if ((DateTime.Now - _hourStart).TotalHours >= 1)
            {
                _hourStart   = DateTime.Now;
                _hourlyCount = 0;
            }

            // Günlük limit kontrolü — aşıldıysa dur
            if (_dailyCount >= MAX_PER_DAY)
            {
                progress.Report($"⛔ Günlük limit ({MAX_PER_DAY}) doldu. Gönderim durduruldu.");
                return false;
            }

            // Saatlik limit kontrolü — dolunca saat dolana kadar bekle
            if (_hourlyCount >= MAX_PER_HOUR)
            {
                int waitMinutes = 60 - (int)(DateTime.Now - _hourStart).TotalMinutes + _random.Next(3, 8);
                progress.Report($"⏳ Saatlik limit ({MAX_PER_HOUR}) doldu. {waitMinutes} dakika bekleniyor...");
                await Task.Delay(TimeSpan.FromMinutes(waitMinutes), ct);
                _hourlyCount = 0;
                _hourStart   = DateTime.Now;
            }

            _hourlyCount++;
            _dailyCount++;
            return true;
        }

        public async Task WaitBetweenMessagesAsync()
        {
            int delaySeconds;
            switch (SeciliHiz)
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
