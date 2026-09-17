# 🚀 Imap to Imap Transfer

[**TR**](#t%C3%BCrk%C3%A7e) | [**EN**](#english)

---

<a name="türkçe"></a>
## 🇹🇷 Türkçe

**ImapToImap_Transfer**, herhangi bir IMAP e-posta servisinden (Yandex Mail, Gmail, Outlook/Office365, cPanel, Custom IMAP, Dovecot vb.) başka bir IMAP e-posta servisine yüksek hacimli (hesap başı 30.000+ e-posta) mailleri güvenli, kesintisiz ve özgün yapısını koruyarak aktarmak için geliştirilmiş masaüstü **C# WinForms (.NET 8.0 / 10.0)** uygulamasıdır.

### 🌟 Öne Çıkan Özellikler

- 🌐 **Evrensel IMAP Desteği:** Yandex, Gmail, Microsoft Outlook, cPanel, Zimbra veya özel sunucular dahil tüm standart IMAP sunucuları arasında mail transferi.
- 🔄 **SQLite İle Kaldığı Yerden Devam Etme (Resume):** Veri aktarımı sırasında internet veya sunucu kopmalarında daha önce aktarılan mailler SQLite veritabanına kaydedilir. Uygulama yeniden başlatıldığında aktarılmış mailler otomatik atlanır (**Skipped**), 0 veri kaybı ile kalınan yerden devam edilir.
- ✉️ **Orijinal MIME ve Tarih Koruma:** E-postalar ham RFC822 MIME formatında aktarılır. Orijinal gönderilme/alınma tarihi (`InternalDate`) ve okundu/okunmadı durumları (`MessageFlags`) eksiksiz korunur.
- ⚡ **Hız ve Bağlantı Koruması (Throttling):** Paket bazlı indirme (Chunked Fetch) ve özelleştirilebilir bekleme süreleri (Delay ms) ile IMAP sunucu limitlerine takılmaz.
- 🔌 **Otomatik Yeniden Bağlanma (Auto-Reconnect):** Zaman aşımı veya ağ kopmalarında IMAP oturumu otomatik olarak sıfırlanır, kimlik doğrulaması yenilenir ve aktif klasör re-open edilerek kesintisiz devam eder.
- ⏸️ **Asenkron Kontrol (Başlat / Duraklat / Devam Et / İptal):** UI thread'i dondurmadan arka planda çalışır. İstediğiniz an transferi duraklatabilir veya iptal edebilirsiniz.
- 📊 **Canlı Durum ve İlerleme Paneli:** Toplam ilerleme çubukları (`ProgressBar`), transfer hızı (mail/sn) ve renk kodlu anlık log ekranı.

### 🔑 Sunucu Bağlantı Notları & Uygulama Şifreleri

İki faktörlü doğrulaması (2FA) açık olan e-posta servislerinde (Yandex, Gmail, Outlook vb.) normal hesap şifresi yerine **Uygulama Şifresi (App Password)** kullanılması gereklidir:
- **Yandex Mail:** [Yandex ID Uygulama Şifreleri](https://id.yandex.com/security/app-passwords) sayfasından `E-posta (IMAP)` şifresi oluşturun.
- **Gmail:** Google Hesabı > Güvenlik > 2 Adımlı Doğrulama > Uygulama Şifreleri altından şifre oluşturun.
- **Diğer IMAP Sunucuları:** Standart e-posta ve IMAP şifrenizi kullanabilirsiniz.

### 🛠️ Kurulum ve Çalıştırma

```bash
git clone https://github.com/okansarii/ImapToImap_Transfer.git
cd ImapToImap_Transfer
dotnet run
```
<img width="993" height="894" alt="image" src="https://github.com/user-attachments/assets/ec55e0a7-cc5a-4fd1-ac5b-0e5f2a25e3a8" />
<img width="984" height="887" alt="image" src="https://github.com/user-attachments/assets/15a4698f-3dfc-4436-9433-f2a89656dd02" />


---

<a name="english"></a>
## 🇬🇧 English

**ImapToImap_Transfer** is a desktop **C# WinForms (.NET 8.0 / 10.0)** application built for high-volume email migration (30,000+ emails per account) between **ANY** IMAP email providers (Yandex Mail, Gmail, Outlook/Office365, cPanel, Custom IMAP, Dovecot, etc.) safely and without data loss.

### 🌟 Key Features

- 🌐 **Universal IMAP Support:** Transfer emails seamlessly between any standard IMAP servers (Yandex, Gmail, Microsoft Outlook, cPanel, Zimbra, or custom private servers).
- 🔄 **SQLite Auto-Resume Capability:** Migrated emails are logged in an embedded SQLite database. In case of network disconnection or app restart, previously transferred emails are instantly skipped (**Skipped**), resuming seamlessly with 0 data loss.
- ✉️ **Preserves Original MIME, Dates & Flags:** Emails are fetched in raw RFC822 MIME format. Original arrival timestamps (`InternalDate`) and read/unread flags (`MessageFlags`) are fully preserved.
- ⚡ **Throttling & Rate-Limit Protection:** Uses chunked summary fetching and customizable delay times (ms) to comply with provider rate limits.
- 🔌 **Automatic Connection Recovery:** Automatically reconnects, re-authenticates, and re-opens target folders whenever TCP/IMAP timeouts or network drops occur.
- ⏸️ **Async Control (Start / Pause / Resume / Cancel):** Runs asynchronously without freezing the UI. Pause or stop migration at any time.
- 📊 **Real-time Progress Dashboard:** Displays overall and folder progress bars, migration speed (mails/sec), and color-coded live logs.

### 🔑 Authentication & App Passwords

When connecting to providers with Two-Factor Authentication (2FA) enabled:
- **Yandex Mail:** Create an `Email (IMAP)` App Password at [Yandex ID Security](https://id.yandex.com/security/app-passwords).
- **Gmail:** Create an App Password under Google Account > Security > 2-Step Verification.
- **Standard IMAP:** Use your regular email address and IMAP password.

### 🛠️ How to Build and Run

```bash
git clone https://github.com/okansarii/ImapToImap_Transfer.git
cd ImapToImap_Transfer
dotnet run
```

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).
