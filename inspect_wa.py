import time
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

# Attach to existing Chrome browser launched by user for WhatsApp Web
options = webdriver.ChromeOptions()
import os
user_data_dir = os.path.join(os.getenv("APPDATA"), "AcentemOto", "ChromeProfiles", "Varsayılan (Default)")

options.add_argument(f"user-data-dir={user_data_dir}")
driver = webdriver.Chrome(options=options)

driver.get("https://web.whatsapp.com/")
print("Waiting for WhatsApp Web to load...")
time.sleep(15)

try:
    print("Finding attachment button...")
    attach_buttons = driver.find_elements(By.CSS_SELECTOR, "div[title='Ekle'], span[data-icon='plus'], span[data-icon='clip']")
    if attach_buttons:
        attach_buttons[0].click()
        time.sleep(2)
        
        file_inputs = driver.find_elements(By.CSS_SELECTOR, "input[type='file']")
        print(f"Found {len(file_inputs)} file inputs.")
        
        for i, inp in enumerate(file_inputs):
            accept = inp.get_attribute("accept")
            displayed = inp.is_displayed()
            print(f"Input {i}: accept='{accept}', displayed={displayed}")
            
    else:
        print("Attachment button not found.")
except Exception as e:
    print(f"Error: {e}")
finally:
    driver.quit()
