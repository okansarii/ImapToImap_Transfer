# 🚀 ImapMigrator - Yandex & Yüksek Hacimli IMAP E-Posta Taşıma Uygulaması

**ImapMigrator**, Yandex IMAP servisinden başka bir IMAP sunucusuna yüksek hacimli (hesap başı 30.000+ e-posta) mailleri güvenli, kesintisiz ve özgün yapısını koruyarak aktarmak için geliştirilmiş masaüstü **C# WinForms (.NET 8.0)** uygulamasıdır.

---

## 🌟 Öne Çıkan Özellikler

- 🔄 **SQLite İle Kaldığı Yerden Devam Etme (Resume):** Veri aktarımı sırasında internet veya sunucu kopmalarında daha önce aktarılan mailler SQLite veritabanına kaydedilir. Uygulama yeniden başlatıldığında aktarılmış mailler otomatik atlanır (**Skipped**), 0 veri kaybı ile kalınan yerden devam edilir.
- ✉️ **Orijinal MIME ve Tarih Koruma:** E-postalar ham RFC822 MIME formatında aktarılır. Orijinal gönderilme/alınma tarihi (`InternalDate`) ve okundu/okunmadı durumları (`MessageFlags`) eksiksiz korunur.
- ⚡ **Yandex Throttling ve Bağlantı Koruması:** Paket bazlı indirme (Chunked Fetch) ve özelleştirilebilir bekleme süreleri (Delay ms) ile Yandex IMAP limitlerine takılmaz.
- 🔌 **Otomatik Yeniden Bağlanma (Auto-Reconnect):** Zaman aşımı veya ağ kopmalarında IMAP oturumu otomatik olarak sıfırlanır, kimlik doğrulaması yenilenir ve aktif klasör re-open edilerek kesintisiz devam eder.
- ⏸️ **Asenkron Kontrol (Başlat / Duraklat / Devam Et / İptal):** UI thread'i dondurmadan arka planda çalışır. İstediğiniz an transferi duraklatabilir veya iptal edebilirsiniz.
- 📊 **Canlı Durum ve İlerleme Paneli:** Toplam ilerleme çubukları (`ProgressBar`), transfer hızı (mail/sn) ve renk kodlu anlık log ekranı.

---

## 📸 Ekran Görüntüleri ve Arayüz

Uygulama arayüzü 2 ana bölümden oluşmaktadır:
1. **Sunucu & Bağlantı Ayarları:** Kaynak ve hedef sunucu IMAP bilgileri, bağlantı test butonları ve performans seçenekleri.
2. **Klasör Seçimi ve Eşleme:** Kaynak klasör listesi, mail sayıları ve hedef sunucudaki karşılık gelen klasör isimleri.

---

## 🛠️ Kurulum ve Çalıştırma

### Gereksinimler
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) veya üzeri

### Çalıştırma Adımları

1. Projeyi klonlayın veya indirin:
   ```bash
   git clone https://github.com/KULLANICI_ADI/ImapMigrator.git
   cd ImapMigrator
   ```

2. Uygulamayı derleyin ve çalıştırın:
   ```bash
   dotnet run
   ```

---

## 🔑 Yandex IMAP Bağlantı Notu

Yandex IMAP erişiminde güvenlik nedeniyle normal hesap şifresi kabul edilmez.
1. [Yandex ID - Uygulama Şifreleri](https://id.yandex.com/security/app-passwords) sayfasına gidin.
2. **E-posta (IMAP)** için yeni bir **Uygulama Şifresi** oluşturun.
3. Uygulamadaki **Kaynak (Yandex)** bölümünde bu şifreyi kullanın.

---

## 📂 Proje Yapısı

```
ImapMigrator/
├── ImapMigrator.csproj           # .NET WinForms Proje Dosyası
├── Program.cs                    # Uygulama Başlangıcı
├── Models/
│   └── TransferModels.cs         # Modeller (ServerConfig, ProgressReport, Options vs.)
├── Services/
│   ├── MigrationStateRepository.cs # SQLite Durum Takip Veritabanı
│   ├── PauseTokenSource.cs         # Asenkron Duraklat/Devam Et Kontrolü
│   └── ImapMigrationEngine.cs      # Core MailKit IMAP Transfer Motoru
└── Forms/
    ├── MainForm.cs               # WinForms Kod Mantığı
    └── MainForm.Designer.cs      # WinForms Tasarım Arayüzü
```

---

## 📄 Lisans

Bu proje [MIT Lisansı](LICENSE) ile lisanslanmıştır.
