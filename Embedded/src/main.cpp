// THIS SHOULD ALWAYS ALWAYS BE HERE
#include <Arduino.h>

#include <SPI.h>
#include <Ethernet.h>

// Enter a MAC address for your controller below.
// Newer Ethernet shields have a MAC address printed on a sticker on the shield
byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };

// if you don't want to use DNS (and reduce your sketch size)
// use the numeric IP instead of the name for the server:
char server[] = "192.168.1.145";    // name address for Google (using DNS)

// Set the static IP address to use if the DHCP fails to assign
IPAddress ip(192, 168, 1, 178);
IPAddress myDns(192, 168, 1, 1);

// Initialize the Ethernet client library
// with the IP address and port of the server
// that you want to connect to (port 80 is default for HTTP):
EthernetClient client;

// Variables to measure the speed
unsigned long beginMicros, endMicros;
unsigned long byteCount = 0;
bool printWebData = true;  // set to false for better speed measurement

//////// TEMPERATURE //////
#include "DHT.h"

#define DHTPIN 4     // Digital pin connected to the DHT sensor

// Type
#define DHTTYPE DHT11   // DHT 11

// Initialize DHT sensor.
DHT dht(DHTPIN, DHTTYPE);
///////////////////////////

void PostTemperature(char temperature[6]){
// start the Ethernet connection:
  Serial.println("Initialize Ethernet with DHCP:");
  if (Ethernet.begin(mac) == 0) {
    Serial.println("Failed to configure Ethernet using DHCP");
    // Check for Ethernet hardware present
    if (Ethernet.hardwareStatus() == EthernetNoHardware) {
      Serial.println("Ethernet shield was not found.  Sorry, can't run without hardware. :(");
      // while (true) {
      //   delay(1); // do nothing, no point running without Ethernet hardware
      // }
    }
    if (Ethernet.linkStatus() == LinkOFF) {
      Serial.println("Ethernet cable is not connected.");
    }
    // try to configure using IP address instead of DHCP:
    Ethernet.begin(mac, ip, myDns);
  } else {
    Serial.print("  DHCP assigned IP ");
    Serial.println(Ethernet.localIP());
  }
  // give the Ethernet shield a second to initialize:
  //delay(1000);
  Serial.print("connecting to ");
  Serial.print(server);
  Serial.println("...");

  // if you get a connection, report back via serial:
  if (client.connect(server, 80)) {
    Serial.print("connected to ");
    Serial.println(client.remoteIP());
    // Make a HTTP GET request:
    // client.println("GET /temperature/getalltemperatures HTTP/1.1");
    // client.println("Host: 192.168.1.145");
    // client.println("Connection: close");
    // client.println();

    
    
  } else {
    // if you didn't get a connection to the server:
    Serial.println("connection failed");
  }
  beginMicros = micros();

  ////// TEMPERATURE /////
  Serial.println(F("DHTxx test!"));

  // Make a HTTP POST request:
    //String postData="{\"value\":\"arduinotest!\",\"device\":\"my AWESOME arduino!\"}";
    //String postData="{\"value\":\"" + temperature + "\",\"device\":\"my AWESOME arduino!\"}";
    
    // TEMPERATURE
    char tempJsonData[600];
    int tempSize = snprintf(tempJsonData, 600, "{\"value\": %s,\"device\":\"my AWESOME arduino!\"}", temperature);
    
    Serial.println(tempJsonData);
    client.println("POST /temperature/PostTemp HTTP/1.1");
    client.println("Host: 192.168.1.145");
    //client.println("User-Agent: Arduino/1.0");
    client.println("Connection: close");
    client.println("Content-Type: application/json");
    client.print("Content-Length: ");
    //client.println(postData.length());
    client.println(tempSize);
    client.println();
    client.println(tempJsonData);
}

void PostHumidity(char humidity[6]){
  // start the Ethernet connection:
  Serial.println("Initialize Ethernet with DHCP:");
  if (Ethernet.begin(mac) == 0) {
    Serial.println("Failed to configure Ethernet using DHCP");
    // Check for Ethernet hardware present
    if (Ethernet.hardwareStatus() == EthernetNoHardware) {
      Serial.println("Ethernet shield was not found.  Sorry, can't run without hardware. :(");
      // while (true) {
      //   delay(1); // do nothing, no point running without Ethernet hardware
      // }
    }
    if (Ethernet.linkStatus() == LinkOFF) {
      Serial.println("Ethernet cable is not connected.");
    }
    // try to configure using IP address instead of DHCP:
    Ethernet.begin(mac, ip, myDns);
  } else {
    Serial.print("  DHCP assigned IP ");
    Serial.println(Ethernet.localIP());
  }
  // give the Ethernet shield a second to initialize:
  //delay(1000);
  Serial.print("connecting to ");
  Serial.print(server);
  Serial.println("...");

  // if you get a connection, report back via serial:
  if (client.connect(server, 80)) {
    Serial.print("connected to ");
    Serial.println(client.remoteIP());
    // Make a HTTP GET request:
    // client.println("GET /temperature/getalltemperatures HTTP/1.1");
    // client.println("Host: 192.168.1.145");
    // client.println("Connection: close");
    // client.println();

    
    
  } else {
    // if you didn't get a connection to the server:
    Serial.println("connection failed");
  }
  beginMicros = micros();

  ////// TEMPERATURE /////
  Serial.println(F("DHTxx test!"));

  // Make a HTTP POST request:
    //String postData="{\"value\":\"arduinotest!\",\"device\":\"my AWESOME arduino!\"}";
    //String postData="{\"value\":\"" + temperature + "\",\"device\":\"my AWESOME arduino!\"}";

    // HUMIDITY
    char humiJsonData[600];
    int humiSize = snprintf(humiJsonData, 600, "{\"value\": %s,\"device\":\"my AWESOME arduino!\"}", humidity);

    Serial.println(humiJsonData);
    client.println("POST /humidity/PostHumidity HTTP/1.1");
    client.println("Host: 192.168.1.145");
    //client.println("User-Agent: Arduino/1.0");
    client.println("Connection: close");
    client.println("Content-Type: application/json");
    client.print("Content-Length: ");
    //client.println(postData.length());
    client.println(humiSize);
    client.println();
    client.println(humiJsonData);
}

void setup() {
  // Open serial communications and wait for port to open:
  Serial.begin(9600);
  while (!Serial) {
    ; // wait for serial port to connect. Needed for native USB port only
  }

  

  dht.begin();
  ///////////////////////
  //char something[6] = "24.45";
  //createPost(something);
}

void loop() {
  // if there are incoming bytes available
  // from the server, read them and print them:
  int len = client.available();
  if (len > 0) {
    byte buffer[80];
    if (len > 80) len = 80;
    client.read(buffer, len);
    if (printWebData) {
      Serial.write(buffer, len); // show in the serial monitor (slows some boards)
    }
    byteCount = byteCount + len;
  }

  // if the server's disconnected, stop the client:
  // if (!client.connected()) {
  //   endMicros = micros();
  //   Serial.println();
  //   Serial.println("disconnecting.");
  //   client.stop();
  //   Serial.print("Received ");
  //   Serial.print(byteCount);
  //   Serial.print(" bytes in ");
  //   float seconds = (float)(endMicros - beginMicros) / 1000000.0;
  //   Serial.print(seconds, 4);
  //   float rate = (float)byteCount / seconds / 1000.0;
  //   Serial.print(", rate = ");
  //   Serial.print(rate);
  //   Serial.print(" kbytes/second");
  //   Serial.println();

  //   // do nothing forevermore:
  //   while (true) {
  //     delay(1000);
  //   }
  // }

  //////// TEMPERATURE /////
  
  // Reading temperature or humidity takes about 250 milliseconds!
  // Sensor readings may also be up to 2 seconds 'old' (its a very slow sensor)
  float h = dht.readHumidity();
  // Read temperature as Celsius (the default)
  float t = dht.readTemperature();
  // Read temperature as Fahrenheit (isFahrenheit = true)
  float f = dht.readTemperature(true);

  // Check if any reads failed and exit early (to try again).
  if (isnan(h) || isnan(t) || isnan(f)) {
    Serial.println(F("Failed to read from DHT sensor!"));
    return;
  }

  // Compute heat index in Fahrenheit (the default)
  float hif = dht.computeHeatIndex(f, h);
  // Compute heat index in Celsius (isFahreheit = false)
  float hic = dht.computeHeatIndex(t, h, false);

  Serial.print(F("Humidity: "));
  Serial.print(h);
  Serial.print(F("%  Temperature: "));
  Serial.print(t);
  Serial.print(F("°C "));
  Serial.print(f);
  Serial.print(F("°F  Heat index: "));
  Serial.print(hic);
  Serial.print(F("°C "));
  Serial.print(hif);
  Serial.println(F("°F"));

  
  char temperature[6];
  char humidity[6];
  dtostrf(t, 4, 2, temperature);
  dtostrf(h, 4, 2, humidity);
  PostTemperature(temperature);
  PostHumidity(humidity);
  // dtostringf
  ////////////////////////////////
  delay(30000);
}

