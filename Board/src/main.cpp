#include "Arduino.h"
#include "main.h"
#include "DHT.h"
#include "ESP8266WiFi.h"
#include "ESP8266WiFiMulti.h"
#include "ESP8266HTTPClient.h"
#include "WiFiClient.h"

const char WIFINAME[] = "IotAP";
const char WIFIPASSWORD[] = "shamsipourIOT";
ESP8266WiFiMulti WiFiMulti;
DHT dht(D7, DHT11);
void setup()
{
  // Configure Serial
  Serial.begin(115200);
  // Clear Serial monitor output
  for (int i = 0; i < 20; i++)
  {
    Serial.println();
  }

  dht.begin();

  WiFi.mode(WIFI_STA);
  WiFiMulti.addAP(WIFINAME, WIFIPASSWORD);
}

void loop()
{
  // Read DHT sensor data:
  float humidity = dht.readHumidity();
  float temperture = dht.readTemperature();
  // Check if reading was successfull:
  if (isnan(humidity) || isnan(temperture))
  {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }

  Serial.printf("Temperture is: %.1fC\nHumidity: %.f%%\n", temperture, humidity);

  Serial.println(F("Sending data to server..."));
  bool success = SendDataToServer(temperture, humidity);
  if (success)
  {
    Serial.println(F("Data transmission was successful."));
  }
  else
  {
    Serial.println(F("Data trasmission failed."));
  }

  delay(5 * (60 * 1000)); // delay 5 minutes
}

bool SendDataToServer(float temperture, float humidity)
{
  while (WiFiMulti.run() != WL_CONNECTED)
  {
    delay (1000);
    Serial.println("Connecting...");
  }
  

  WiFiClient wifiClient;
  HTTPClient httpClient;
  int statusCode = -100;

  char buffer[32];
  sprintf(buffer, "/send?temp=%.1f&humi=%.f", temperture, humidity);
  if (httpClient.begin(wifiClient,"de.gorazbang.ga",80,buffer,false))
  {

    statusCode = httpClient.GET();
    Serial.println(statusCode);
  }
  else
  {
    Serial.println("FUCK");
  }
  return statusCode == 200;
}
