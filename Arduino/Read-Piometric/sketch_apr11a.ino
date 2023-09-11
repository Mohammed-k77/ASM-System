const int analog_pin = A0;
int sensorValue;
void setup() {
 Serial.begin(9600);

}
 
void loop() {
  // 10-bit , (0 -> 1023)
unsigned int analog_val = 0;
analog_val = analogRead(analog_pin);
 Serial.println(analog_val);
 delay(1000);
}
