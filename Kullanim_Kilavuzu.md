# AcentemOto WhatsApp Otomasyonu - Kullanım Kılavuzu 🚀

Bu kılavuz, **AcentemOto** uygulamasının tüm özelliklerini, menülerini, şablon sistemini ve acente ağınız (NAS / Ortak Klasör) içerisindeki doğru kullanım koşullarını barındıran temel rehberdir.

---

## 🏗️ 1. Ortak Ağ (NAS) ve Veritabanı Kullanım Kuralları

Uygulamanız veritabanı olarak hiçbir harici sunucuya ihtiyaç duymayan, tamamen taşınabilir olan **SQLite** kullanmaktadır. Uygulamanın kurulu olduğu klasörü (veya `messages.db` dosyasını) NAS cihazınız üzerinden acente ortak ağına koyabilirsiniz.

> [!WARNING]
> **ÇOK ÖNEMLİ (AĞ KULLANIM KURALI)**
> Program ortak ağda çalışırken; ofisteki herkes programı **aynı anda açabilir**, filtreleme yapabilir, excel raporları indirebilir ve müşteri durumlarını inceleyebilir. Ancak sistemin kilitlenmemesi için **aynı anda sadece 1 bilgisayar WhatsApp'tan "Gönderimi Başlat"** butonuna basmalıdır. (Geri kalanlar o esnada sistemde izleyici konumunda/raporlama ekranında kalmalıdır).

---

## 📊 2. Excel Yükleme ve Parametre Okuma

Müşteri datanızı (`.xlsx` formatında) uygulamaya yüklerken en üst satırda mutlaka başlıklarınız olmalıdır. Program bu başlıkları okur ve onlara göre işlem yapar.

*   **Zorunlu Başlık:** Müşterinin telefon numarasının yer aldığı sütunun adı `Telefon`, `Numara` veya `Referans` olmalıdır. (Numaralar 0555..., 555..., +90555... gibi farklı formatlarda olabilir, sistem Türkiye kodunu `905...` otomatik ayarlar).
*   **Değişkenler:** Excel'deki diğer tüm sütun başlıkları mesaj kutusunda `{}` (Süslü parantez) içerisinde bir metin değişkeni olarak kullanılabilir. *(Örn: Excel'de "Ad Soyad" sütunu varsa, metne `{Ad Soyad}` yazabilirsiniz).*
*   **Özel Alanlar:** Kategori ve Kampanya sekmesinin tam verimli çalışması için `Tür` (veya `Tur`), `Şirket`, `Sigorta Tarihi` gibi sütunları Excel dosyanıza eklemeniz tavsiye edilir.

---

## 📑 3. Şablon (Template) Yönetimi

Her seferinde aynı uzun metinleri baştan yazmamak için akıllı şablon özelliğini kullanın.
1.  **Şablon Hazırlayın:** Metin kutusuna mesajınızı değişkenleriyle birlikte (Örn: `Sayın {Ad Soyad}, {Plaka} aracınız...`) yazın.
2.  **Şablon Olarak Kaydet:** Metin kutusunun altındaki kayıt ikonuna tıklayın, şablona bir isim verin (Örn: "Haziran Trafik Hatırlatması").
3.  **Kullanım:** Uygulamaya her girdiğinizde yukarıdaki Açılır Menüden kaydettiğiniz şablonu seçip metin kutusuna bir saniyede getirebilirsiniz.

> [!TIP]
> **Önizleme Alın:** Mesajı göndermeden önce mutlaka **"Önizle (Göz İkonu)"** butonuna basın. Bu sayede program, Excel'deki ilk kişinin verilerini çekip `{Ad Soyad}` gibi değişkenlerin yerine gerçek kişinin adını koyar ve müşteriye bu mesajın tam olarak nasıl gideceğini size sahnede gösterir.

---

## 📱 4. Ana Sekmeler ve Modüller

### Sekme 1: Gönderim Ekranı (Genel Gönderim)
WhatsApp oturumu açıp, yüklediğiniz Excel içerisindeki tüm listeye veya manuel eklediğiniz kişilere genel bir şablon ile mesaj göndermek içindir.
*   **Bağlan:** Chrome sekmesi üzerinden WhatsApp Web'i açar.
*   **Gönderimi Başlat / Durdur:** İstediğiniz an gönderimi iptal edebilirsiniz.
*   **Medya Ekle:** Mesajların yanında Sigorta Teklif PDF'leriniz veya Kampanya görsellerinizi (JPG, PNG) ekleyebilirsiniz.

### Sekme 2: Kategori Gönderimi
Listeyi daraltmak ve nokta atışı gruba mesaj atmak içindir.
*   *Örnek Kullanım:* Müşteriniz çok ama siz sadece bitiş tarihi **01 Haziran - 30 Haziran** olan ve Türü **Trafik** olanlara mesaj atacaksınız. Excel'i yüklersiniz, Sol menüden Kategori -> Sigorta Tarihi ve ardından Türü filtrelerini uygularsınız. Yalnızca hedeflenen müşteriler alttaki mini listeye düşer ve sadece onlara mesaj gider.

### Sekme 3: Kampanyalar ve Çapraz Satış (Cross-Sell)
Bu sekme uygulamanın **en akıllı** bölümüdür. Müşterilerinize hali hazırda olmayan ürünleri satmanız (çapraz satış) için tasarlanmıştır.

1.  **Excel'e Fiyat Girin:** Excel'inizde `Anadolu`, `Sompo`, `Ak` gibi sigorta şirketi başlıkları açıp altına TL olarak fiyat yazın.
2.  Uygulama bu fiyatlara bakar, en ucuz olanı bulur ve `{EnUygunFiyat}` ile `{EnUygunSirket}` adında iki otomatik değişken yaratır.
3.  Kampanya Hedefi seçin (Örn: *Trafik'ten Kasko'ya*). Sadece trafiği olup kaskosu olmayanları otomatik ayırır.
4.  Şablonunuzu yazın: `"Sayın {Ad Soyad}, Trafik müşterimizsiniz. Sizin aracınıza en uygun Kasko poliçesini {EnUygunFiyat} bedelle {EnUygunSirket} şirketinden bulduk."`
5.  **Rapor Al / Export:** Bu hedeflenen müşteri kitlesini bir **"Eksik Poliçe Excel Raporu"** olarak butona tıklayıp dışarı aktarabilir; personelinizin bu listeyi ayrıca aramasını sağlayabilirsiniz.

### Sekme 4: Dashboard
Toplam kaç kişiye mesaj atıldığı, kaçının ulaştığı, kaçının hata verdiğiyle ilgili detaylı ve renkli bir analiz sayfasıdır. İş bitiminde raporlama için kullanılır.

---

## 🛡️ 5. Güvenlik, Anti-Spam ve Engel Aşma (Ban Koruma)

WhatsApp numaranızın engellenmemesi (Ban yememesi) için programın arkasında çok akıllı bir Anti-Spam algoritması çalışır:

1.  **Saatlik ve Günlük Limit:** Sistem 1 saatte maksimum 45, Günde ise maksimum 250 mesaja izin verecek şekilde programlanmıştır. Eğer kota dolarsa program kendini duraklatır ve bekler.
2.  **Dinamik Hız (Rastgele Süreler):** Gönderim Hızı Seçiciden (Hızlı, Orta, Yavaş) "Orta" yı seçmeniz önerilir. Program iki mesaj arasında insan davranışı sergileyerek 12 ila 29 saniye arasında rastgele saniyelerde gönderir.
3.  **Unique Hash (Benzersiz Mühür):** WhatsApp arka planda kopya (spam) mesajları yakalar. Kutucuktaki "Mesaj sonuna benzersiz kod ekle" özelliğini açtığınızda program, her müşterinin sonuna görünmez kısa bir kod/harf ekler. WhatsApp her bir mesaja "Kişiye özel eşi benzeri olmayan bir metin" muamelesi yapar.
4.  **Ağ ve İnternet Kontrolü:** İnternetiniz kopsa bile program hata vermez; Google sunucularına sürekli bağlantı denemesi (Ping) yollayarak gizlice internetinizin geri gelmesini bekler. Geldiğinde kaldığı yerden (WhatsApp Web log-out olmadıysa) devam eder.

> [!NOTE]
> Bu kılavuz yeni özelliklere göre güncellenmiştir. Takıldığınız bir noktada ekrandaki bildirimleri takip edin. İyi çalışmalar dileriz! 🚀

---

## 📗 6. Örnek Excel Tablosu Tasarımları

Uygulamanın gücünden tam faydalanabilmek için Excel (`.xlsx`) dosyanızı doğru hazırlamak çok önemlidir. İlk satır mutlaka başlıkları içermeli, altındaki satırlar ise verilerden oluşmalıdır.

### 📌 Basit (Genel) Gönderim Excel'i
Sadece bayram tebriği veya standart bir duyuru atmak için gereken en basit formattır. Yalnızca Telefon numarasının tespit edilmesi yeterlidir.

| Telefon | Ad Soyad |
| :--- | :--- |
| 0555 123 45 67 | Ahmet Yılmaz |
| 0500 987 65 43 | Ayşe Demir |
| +90 532 111 22 33 | Mehmet Bey |

**Mesaj Şablonu Örneği:** `Sayın {Ad Soyad}, bayramınızı içten dileklerimizle kutlarız.`

### ⚙️ Kategori (Gelişmiş) Filtreleme Excel'i
Filtreleme sisteminin (Sigorta Tarihleri, Türler ve Şirket) sağlıklı çalışabilmesi için sütunlarınız bu şekilde olmalıdır:

| Telefon | Ad Soyad | Plaka | Tür | Sigorta Tarihi | Şirket |
| :--- | :--- | :--- | :--- | :--- | :--- |
| 5551234567 | Ali Veli | 34XYZ99 | Trafik | 15.06.2024 | Anadolu Sigorta |
| 05329998877 | Hakan Şükür | 06ABC01 | DASK | 20.06.2024 | Türk Nippon |
| 05441112233 | Merve Çelik | 35DEF12 | Kasko | 25.07.2024 | Allianz |

* **Nasıl Filtrelenir:** Örneğin menüden "Kategori: Tür", değerini "Trafik" seçerseniz sadece Ali Veli listeye düşer. Veya "Sigorta Tarihi: 15 Haz - 25 Haz" seçerseniz Ali ve Hakan listelenir.

### 💰 Çapraz Satış (Kampanya) Excel'i - OTOMATİK FİYAT BULUCU
Müşterinin henüz almadığı ek poliçeleri satmak için **yan yana sigorta şirketi adında sütunlar** açıp altlarına fiyatlarını girdiğiniz formattır.

| Telefon | Ad Soyad | Plaka | Tür | Mevcut Durum | Anadolu Sigorta | Sompo Sigorta | Ak Sigorta |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 5551112233 | Veli Bey | 34AA11 | Trafik | Yenilenecek | 4.500 | 4.250 | 4.600 |
| 5554445566 | Zeynep Hanım| 06BB22 | Kasko | Düşünüyor | 12.000 | 11.500 | 11.800 |

* **Programın Yaptığı Büyü:** Veli Bey için fiyatlar (4500, 4250, 4600) okunur. Sistem en ucuzunu otomatik bulur.
* **Görünmez Değişkenler:** Program Veli Bey için `{EnUygunFiyat}` değerini **"4.250"**, `{EnUygunSirket}` değerini ise **"Sompo Sigorta"** olarak hafızaya alır.
* **Akıllı Şablon Örneği:** `Sayın {Ad Soyad}, Trafik poliçenizi yenilemek için şirketimizi tercih ettiniz. Sistemin bulduğu en avantajlı fiyat {EnUygunSirket} firmasından {EnUygunFiyat} TL ile sunulmuştur!`
