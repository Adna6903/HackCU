//Input pins for switch
#define SW_1 9
#define SW_cam 8

#define jump 13
#define attack 12 
#define gun 11

//Input pins for joystic
#define joy_x A0 
#define joy_y A1

//Input pins for camera
#define cam_x A2
#define cam_y A3

void setup(){
  Serial.begin(9600);
  
  pinMode(SW_1, INPUT_PULLUP);
  pinMode(SW_cam, INPUT_PULLUP);

  pinMode(jump, INPUT_PULLUP);
  pinMode(gun, INPUT_PULLUP);
  pinMode(attack, INPUT);

  pinMode(joy_x, INPUT);
  pinMode(joy_y, INPUT);

  pinMode(cam_x, INPUT);
  pinMode(cam_y, INPUT);
}

void loop(){
  //Reading the x, y values for joystic
  float joy_rx= analogRead(joy_x);
  float joy_ry= analogRead(joy_y);
  
  //Reading the x, y values for camera
  float cam_rx= analogRead(cam_x);
  float can_ry= analogRead(cam_y);

  //Mapping the values of joystic
  joy_rx= map(joy_rx, 0, 1023, -10, 10);
  //Max: -1
  //Mid: -4
  //Min: -7 

  joy_ry= map(joy_ry, 0, 1023, -10, 10);
  //Max: -2
  //Mid: -4
  //Min: -6
  
  //Mapping the values of camera
  cam_rx= map(cam_rx, 0, 1023, -10, 10);
  //Max: 2
  //Mid: -4
  //Min: -10
  can_ry= map(can_ry, 0, 1023, -10, 10);
  //Max: 2
  //Mid: -4
  //Min: -10

  //Sending values in a CSV format to 
  //Format: j1x, j1y, camx, camy, sw1, sw2
 Serial.print(joy_rx);
  Serial.print(",");
 Serial.print(joy_ry);
  Serial.print(",");

 Serial.print(cam_rx);
  Serial.print(",");
   Serial.print(can_ry);
  Serial.print(",");

  Serial.print(!digitalRead(SW_1));
  Serial.print(",");
  Serial.print(!digitalRead(SW_cam));
  Serial.print(",");
  Serial.print(digitalRead(jump));
  Serial.print(",");
  Serial.print(digitalRead(gun));
  Serial.print(",");
  Serial.print(digitalRead(attack));
  
  Serial.print("\r\n");
  Serial.flush();
  
}