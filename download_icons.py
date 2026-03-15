import os
import urllib.request

icons_dir = "Icons"
if not os.path.exists(icons_dir):
    os.makedirs(icons_dir)

icons = {
    "upload.png": "https://raw.githubusercontent.com/google/material-design-icons/master/png/file/folder_open/materialicons/24dp/2x/baseline_folder_open_black_24dp.png",
    "download.png": "https://raw.githubusercontent.com/google/material-design-icons/master/png/file/download/materialicons/24dp/2x/baseline_download_black_24dp.png",
    "connect.png": "https://raw.githubusercontent.com/google/material-design-icons/master/png/communication/vpn_key/materialicons/24dp/2x/baseline_vpn_key_black_24dp.png",
    "send.png": "https://raw.githubusercontent.com/google/material-design-icons/master/png/content/send/materialicons/24dp/2x/baseline_send_black_24dp.png",
    "stop.png": "https://raw.githubusercontent.com/google/material-design-icons/master/png/av/stop/materialicons/24dp/2x/baseline_stop_black_24dp.png",
    "photo.png": "https://raw.githubusercontent.com/google/material-design-icons/master/png/editor/insert_photo/materialicons/24dp/2x/baseline_insert_photo_black_24dp.png"
}

for filename, url in icons.items():
    filepath = os.path.join(icons_dir, filename)
    print(f"Downloading {filename}...")
    try:
        urllib.request.urlretrieve(url, filepath)
        print(f"Saved to {filepath}")
    except Exception as e:
        print(f"Failed to download {filename}: {e}")

print("Done downloading icons.")
