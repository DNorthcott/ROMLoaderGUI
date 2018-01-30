# ROMLoaderGUI

## Summary
The ROMLoader is a C# application designed to assist with fleet management in a coal mine.  

This is a personal project to assist myself in learning C# and connecting a system up to 
a database.  In this case SQLite is used as a local database.

### Problem scope

Coal mines produce a product to a specific spec, however the coal seams in a deposit
may not meet these requirements alone.  By blending different seams together through 
feeding coals into the Coal Processing Plant in a set order the product specification 
can be met.

To achieve this, it often that coals are stockpiled seperatly.  By stockpiling coal
there is an increase in rehandle.  Rehandling of coals has been shown to increase 
coal fines which is more expensive process.  

To meet keep the blend cycle the required coal that needs to be fed may not be coming
from the pit, in this case the coal is loaded by a loader at the Run Of Mine (ROM).
In the current environment the loader operator is not aware of the coal movements from 
the pit to the ROM.  This causes the feeding of the coal to be often inefficient.

For example a truck with the required coal will arrive at the ROM in 5 minutes, the operator
of the loader is unware of this and will load the coal from the stockpile to be put into the bin.
The truck coming from the pit is no longer the required coal and is dumped onto the stockpile.
If the loader operator was aware of this trucks arrival time with the coal this rehandle could of been
avoided.

### What this project achieves?

As mentioned above this was made for learning purposes, hence only a part of the system was 
created.

This project allows for coal movements, blends, stockpile allocations to be stored in a database
that can be accessed to provide information to the ROM loader.  The system when allocating coals
updates the database so that it is recorded which coals where directly fed opposed to dumped onto
a stockpile.


The GUI displays the blend, stockpiles and trucks that have been allocated to directly feed into the
CPP bin.  It also takes input from the operator for changing the maximum time a truck can wait before
dumping its coal and change the time for the loader to load coal.


### How coal movements are allocated.
The system allows the loader to determine the maximum wait time, the required time to load coal.

When the load coal button is clicked, the system will make a call to the database getting all 
coal movements that are coming from the pit.  If the required coal is in this list and arrives between
the start time (load button clicked) and the (start time + load time) it is allocated to the bin.

This process is done again however the start time is moved forward to the point of when the above 
allocated truck dumped its load.  In some cases multiple trucks can be allocated causing the ROM
loader be idle for some time as the blend cycle can be met by the incoming trucks.

The maximum wait time allows trucks to wait if they are an required coal but arrive out of order.
This time can be changed to either reduce rehandle or maximise truck utilisation.