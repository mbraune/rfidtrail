# rfidtrail
read multiple FTDI based RFID reader modules

windows forms app with self-explaining GUI

<h4>List button</h4>
list available FTDI devices (sensors)<br/>
enable checkboxes
<h5>checkboxes</h5>
select up to 10 sensors<br/>
optional enable 2 multiplexed sensor groups 
<h4>Open button</h4>
open sensors<br/>
disable checkboxes
<h4>Start button</h4>
start asynchronous Backgroundworker for reading sensors<br/>
if read event, log timestamp, sensorID and detected RFID tag to text file<br/>
        
