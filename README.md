# AcentemOto WhatsApp Otomasyonu

AcentemOto, sigorta acentelerine yönelik geliştirilmiş profesyonel bir WhatsApp otomasyon aracıdır. Bu araç aracılığıyla müşteri datanızı (Excel) içeri aktarabilir, süre dolum tarihlerine göre bildirim mesajları atabilir ve gelişmiş **Çapraz Satış & Kampanya** özelliklerini kullanabilirsiniz. Anti-Spam motoru sayesinde mesajlarınız insani hızlarda ve rastgele benzersiz parametrelerle (Unique Hash) gönderilir.

---

## 📊 Önerilen Excel Dosyası Formatı

Uygulamanın akıllı analiz motorunun müşteri listenizi en iyi şekilde okuyabilmesi için Excel (`.xlsx`) dosyanızın ilk satırında **sütun başlıkları** bulunmalıdır. AcentemOto, sütun başlıklarına bakarak dinamik değişkenler oluşturacaktır.

### Zorunlu Olan Tek Sütun: Telefon Numarası
Müşteriye ulaşılacak numara sütununun başlığı şunlardan biri olmalıdır (büyük/küçük harf duyarsızdır):  
`Telefon`, `Numara`, `Phone`, `Referans / Telefon`, veya `Referans`.

### Gelişmiş Şablon ve Filtreleme İçin Önerilen Diğer Sütunlar
Otomasyonun filtrelerini (Kategori Gönderimi ve Kampanyalar) tam performansta kullanmak için aşağıdaki sütun isimlerini (başlıkları) de ekleyebilirsiniz:

| Sütun Başlığı | Örnek Veri | Açıklama |
| --- | --- | --- |
| **Ad Soyad** | Ahmet Yılmaz | Mesajda `{Ad Soyad}` veya `{Ad}` parametresini kullanmak için. |
| **Plaka** | 34ABC123 | Şablonlarda `{Plaka}` verisi kullanmak için. |
| **Sigorta Tarihi** | 01.05.2024 | "Kategori Gönderim" sekmesinde belirli tarih aralıklarına filtre yapmak için (veya _Sigorta_Tar_). |
| **Tür** (veya **Tur**) | Trafik, Kasko, TSS, DASK | Kampanya çapraz satışında ve Kategori filtresinde poliçe türünü yakalamak için çok kritiktir. |
| **Şirket** | Anadolu, Allianz, Ak | Kategori sekmelerinde sigorta şirketine göre filtreleme oluşturur. |
| **(Şirket Adı İle Fiyatlar)** | 1.500,00 | Eğer yan yana şirket isimleri açıp (Örn: *Anadolu, Sompo, Ak*) altlarına fiyat yazarsanız uygulama **Otomatik En Uygun Teklifi ve Şirketi Çıkarır!** (`{EnUygunSirket}`, `{EnUygunFiyat}`) parametrelerine dönüştürür. |

**Örnek Basit Excel Tablosu Görünümü:**
| Ad Soyad | Telefon | Plaka | Tür | Sigorta Tarihi | Anadolu Sigorta | Sompo Sigorta |
| --- | --- | --- | --- | --- | --- | --- |
| Ali Veli | 0555 123 45 67 | 34XYZ99 | Trafik | 15.06.2024 | 4.500 TL | 4.250 TL |
| Ayşe Hanım | 0500 987 65 43 | 06ABC01 | İMM | 20.06.2024 | 1.200 TL | 1.500 TL |

**Önemli Not:** Telefon numaraları her formatta yazılabilir (`5551234567`, `0 555 123 45 67`, `+905551234567`). Sistem otomatik olarak Türkiye standartına (`905...`) çevirecektir.

---

## Modüllerin Detaylı Kullanımı

### 1. Temel Gönderim Ekranı 
Bu sekme, yüklü olan tüm Excel listesine manuel veya tek tip zamanlanmış bir şablon göndereceğiniz zaman kullanılır.
- **Excel Yükle** butonuna tıklayıp rehber listenizi ekleyin.
- WhatsApp oturumunuzu başlatmak için **WhatsApp Bağlan** butonuna basın ve (gerekliyse) telefondan QR kodunu okutun.
- Metin kutusuna `{Ad Soyad}`. `{Plaka}` gibi excel başlıklarınızı kıvırcık parantez içinde yazarak dinamik metin oluşturun.
- Gönderim hızını (Orta: 12-29 saniye arası rastgele) seçerek **Gönderimi Başlat**'a tıklayın. 

### 2. Kategori Gönderim
Bu panel, yalnızca belirli şartları karşılayan kişilere mesaj atmanıza yarar (Örn: DASK'ı bu hafta bitenler veya sadece Sompo Sigorta müşterileri gibi).
- Sol üstteki listeden kriter seçin: **Sigorta Tarihi**, **Tür**, **Şirket** vb.
- Tarih aralığını veya kutucuktan değeri belirleyip **Filtrele** butonuna tıklayın. Süzülen müşteriler tabloya düşer.
- Sadece süzülmüş portföyünüz için şablonunuzu yazıp işlemi başlatın.

### 3. Kampanya & Çapraz Satış (Cross-Sell) Modülü
Bu modül, mevcut poliçe listelerinize bakarak, müşterilerinize **sahip olmadıkları poliçeleri (Örn: Trafik var ama Kasko yok)** satmayı teklif ettiğiniz modüldür. Kârlılığı arttırmak içindir.

1. **Kampanya Türünü Belirleyin:** Açılır menüden stratejinizi seçin (Örn: "Trafik'ten İMM'ye"). Sistem algoritmik olarak, listesinde bir "Trafik" poliçesi bulunan ancak "İMM" ile ilgili poliçesi olmayan müşterileri hemen ayrıştırır.
2. **Akıllı Şablon Kullanımı:** Excel'e girmiş olduğunuz tüm teklifler arasından en uygun fiyatı çıkartıp metne ekleyebilirsiniz:
   `Sayın {Ad Soyad}, en avantajlı İMM poliçenizi {EnUygunFiyat} bedelle {EnUygunSirket} şirketinden hazırladık.`
3. **Ek Reklam Satırı (Ad):** Metnin en sonuna boşluk bırakılarak sabit bir kampanya notu (Örn: `Tüm kartlara vade farksız 3 taksit!`) ekleyebilirsiniz.
4. Hedef filtrelemesi başarılı olduğunda alttaki tablodan müşteri kontrolü yapabilir ve sonrasında **Kampanya Gönder** ile tüm potansiyel fırsatlara mesajlarınızı yollayabilirsiniz.

---

### Güvenlik ve Anti-Spam
WhatsApp hesabınızın banlanmaması için şu iki kuralla çalışın:
- Asla "Hızlı" gönderim limitini uzun Excel listelerinde zorlamayın (önerilen Orta hız).
- **"Mesajın sonuna benzersiz kod ekle"** (Unique Hash) kutucuğunu açık bırakın. Bu sayede program her müşterinin cümlesinin sonuna görünmez kısa yollar atar, WhatsApp algoritması metnin kopyala/yapıştır olmadığını, elle tek tek yazıldığını zannederek spam kalkanlarını indirir.
