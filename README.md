# rftrail
read multiple FTDI based RFID reader modules

windows forms app with self-explaining GUI<br />

<h4>steps</h4> 
1. list available FTDI devices (sensors)<br />
2. select up to 10 sensors<br />
3. open selected sensors <br />
4. start asynchronous Backgroundworker for reading sensors<br />
5. if read event, log timestamp, sensorID and detected RFID tag to text file<br />
        
