;******************************************************************************************
;Program Name: proj2.asm
;Programmer:   Your name here
;Class:        CSCI 2160-001
;Date:         March, 2 2017 at 08:00 AM
;Purpose:
;	making system time calls and displaying the difference in both standard time and 
;	in milliseconds. Also, afte the first two time calls are made to the system clock.
;   asking the user to enter first an earlier time of hours, minutes, seconds, and
;   milliseconds then asking them to enter a second later time call. Finally displaying 
;   those differences the same way as the first two system calls. The user is allowed to 
;   repeat this two time call process until at any time they only press the ENTER key.
;******************************************************************************************

	.486
	.model flat
	.stack 100h
	
	ExitProcess     PROTO Near32 stdcall, dwExitCode:dword
	ascint32	    PROTO Near32 stdcall, lpStringToConvert:dword 
	intasc32	    PROTO Near32 stdcall, lpStringToHold:dword, dval:dword
	getstring	    PROTO Near32 stdcall, lpStringToGet:dword, dlength:dword
	getch			PROTO Near32 stdcall
	putch		    PROTO Near32 stdcall, bChar:byte
	putstring 	    PROTO Near32 stdcall, lpStringToPrint:dword
	GetLocalTime	PROTO Near32 stdcall, lpSystemTimeData:dword
	intasc	        PROTO Near32 stdcall, lpStringToHoldASCII:dword, sVal:word
	intasc32Comma PROTO Near32 stdCall, lpStringToHoldNumericChars:dWord,dVal:dword;
	
puts macro param1 ;macro to display a string

		push ebx				;save ebx
		lea ebx,[param1]		;used incase param1 is a register
		INVOKE putstring,ebx	;display the string
		pop ebx					;restore ebx
	 endm
gets	macro strToHold,bNum ;macro to get a string from the user
	push	ebx		;preserve EBX
	push	ECX		;preserve ECX
	lea	ebx,[strToHold]	;because the param could be a register with an address
	mov	ecx,0			;clear ECX 
	mov	cl,bNum			;store the size of the string you wish to get
	INVOKE getstring, ebx, ecx ;get the string from the user
	POP ECX		;restore ECX
	POP EBX		;restore EBX
	endm
newLine macro ;add a new line to the program
			INVOKE putch,10 ;insert new line
			INVOKE putch,13 ;return cursor
		endm
		
tab macro ;tab over
	INVOKE putch, 9 ;move cursor over 1 tab
	endm
	
	.data
;All data identifiers start in cc 1 and the types line up where possible.
iDiff         	  dWord ?		;holds the time difference in millisecond
systemTimeData	  label word ;label to mark the first System Time Call
wYear	          word	?	;holds system year
wMonth	          word	?	;holds system month
wDayOfWeek	      word	?	;holds system DayOfWeek
wDay	          word	?	;holds system Day
wHour	          word	?	;holds system Hour
wMinute	          word	?	;holds system minute
wSecond	          word	?	;holds system second
wMillisecs	      word	?	;holds system millisecond

systemTimeData2	  label word ;label to mark the second system time call
wYear2	          word	?	;holds system year
wMonth2	          word	?	;holds system month
wDayOfWeek2	      word	?	;holds system DayOfWeek
wDay2	          word	?	;holds system Day
wHour2	          word	?	;holds system Hour
wMinute2	      word	?	;holds system minute
wSecond2	      word	?	;holds system second
wMillisecs2	      word	?	;holds system millisecond

systemTimeDataDiff	  label word	;lable to mark the difference in the two time calls
wYearDiff	          word	?	;holds system year
wMonthDiff	          word	?	;holds system month
wDayOfWeekDiff        word	?	;holds system DayOfWeek
wDayDiff	          word	?	;holds system Day
wHourDiff	          word	?	;holds system Hour
wMinuteDiff	          word	?	;holds system minute
wSecondDiff	          word	?	;holds system second
wMillisecsDiff	      word	?	;holds system millisecond

strHeading	      byte  10,9,"********************************************"
				  byte	10,9,"*       Name: Justin Hopkins               *"
				  byte	10,9,"*      Class: CSCI 2160-001                *"
				  byte	10,9,"*       Date: March, 2 2017                *"
				  byte	10,9,"*        Lab: Proj2                        *"
				  byte  10,9,"********************************************",10,13,0
strYear			  byte  5 dup(?)   ;holds the ascii value of the year
strMonth		  byte  3 dup(?)   ;holds the ascii value of the month
strDayOfTheWeek   byte  1		;holds the ascii value for the day of the week		   
strDay			  byte  3 dup(?) ;holds the ascii value of the day of the month
strHour			  byte  3 dup(?) ;holds the ascii value of the hours
strMinute 		  byte  3 dup(?) ;holds the ascii value for the minutes
strSecond		  byte  3 dup(?) ;holds the ascii value for the seconds
strMillisecs	  byte  5 dup(?) ;holds the ascii value for the Milliseconds
strPrompt1		  byte	10,13,9,9,"Enter Earlier Time ",0
strPrompt2		  byte	10,13,9,9,"Enter Later Time",0
strPrompt3		  byte	10,13,9,9,"       Hours: ",0
strPrompt4		  byte	10,13,9,9,"     Minutes: ",0
strPrompt5		  byte	10,13,9,9,"     Seconds: ",0
strPrompt6		  byte	10,13,9,9,"     Milliseconds: ",0
strTime1		  byte  10,13,9,9,"    Time 1: ",0
strTime2          byte  10,13,9,9,"    Time 2: ",0
strDifference     byte  10,13,9,9,"Difference: ",0
strDiff     byte  13 dup(?)	;holds the ascii value of the diff. in milliseconds with commas
strElapsed		  byte  10,13,9,9,"The elapsed time in millisecs is: ",0   

	.code	
_start: 
	mov eax,0					;dummy executable statement to aid in debugging.
	tab
	INVOKE putstring, ADDR strHeading		;Display programmer info
	INVOKE GetLocalTime, ADDR systemTimeData ;get system time store it in systemTimeData
	;test loop takes approximatly 4 seconds. Used to test difference in system time calls.
	mov ecx,1 ;set loop counter to one.
stOuterLoop:
	mov eax,ecx ;save loop counter 
	mov ecx,0   ;set loop counter to zero
stInnerLoop:
	Loop stInnerLoop ;end of inner loop
	mov ecx, eax ;restore ecx
	LOOP stOuterLoop ;end outer loop
	INVOKE GetLocalTime, ADDR systemTimeData2;second system time call => systemTimeData2	
displayTime:
	newLine 						   ;adds blank line
	puts strTime1                      ;display "Time 1:"
	INVOKE intasc, ADDR strYear, wYear ;convert year value from 1st time call for display
	INVOKE putstring,ADDR strYear	   ;display the year
	INVOKE putch ,':'				   ;display a colon
	INVOKE intasc, ADDR strMonth, wMonth ;convert month from 1st time call for display
	INVOKE putstring,ADDR strMonth		 ;display the month
	INVOKE putch ,':'					 ;display a colon
	INVOKE intasc, ADDR strDayOfTheWeek, wDayOfWeek ;convert day of week for display
	INVOKE putstring,ADDR strDayOfTheWeek ;display the day of the week
	INVOKE putch ,':'					  ;display a colon
	INVOKE intasc, ADDR strDay, wDay	  ;convert the day 1st call for display
	INVOKE putstring,ADDR strDay		  ;display the day
	INVOKE putch ,':'					  ;display a colon
	INVOKE intasc, ADDR strHour, wHour	  ;convert hour 1st call for display
	INVOKE putstring,ADDR strHour		  ;display hour
	INVOKE putch ,':'					  ;display colon
	INVOKE intasc, ADDR strMinute, wMinute ;convert minute 1st call for display
	INVOKE putstring,ADDR strMinute		   ;display minute 
	INVOKE putch ,':'					   ;display colon
	INVOKE intasc, ADDR strSecond, wSecond ;convert seconds 1st call for display
	INVOKE putstring,ADDR strSecond		   ;display seconds
	INVOKE putch ,':'					   ;display colon
	INVOKE intasc, ADDR strMillisecs, wMillisecs ;convert milliseconds 1st call for display
	INVOKE putstring,ADDR strMillisecs	   ;display milliseconds
	
	puts strTime2						;display "Time 2: "
	INVOKE intasc, ADDR strYear, wYear2 ;convert 2nd year to ascii value for display
	INVOKE putstring,ADDR strYear		;display 2nd year
	INVOKE putch ,':'					;display a colon
	INVOKE intasc, ADDR strMonth, wMonth2 ;convert 2nd month to ascii value for display
	INVOKE putstring,ADDR strMonth		  ;display 2nd month
	INVOKE putch ,':'					  ;display a colon
	INVOKE intasc, ADDR strDayOfTheWeek, wDayOfWeek2;convert 2nd day of week to ascii
	INVOKE putstring,ADDR strDayOfTheWeek			;display 2nd day of week
	INVOKE putch ,':'					  			;display a colon
	INVOKE intasc, ADDR strDay, wDay2	 ;convert 2nd day of month to ascii for display
	INVOKE putstring,ADDR strDay		 ;display 2nd day of month
	INVOKE putch ,':'					 ;display a colon
	INVOKE intasc, ADDR strHour, wHour2	 ;convert 2nd hour to ascii for display
	INVOKE putstring,ADDR strHour		 ;display 2nd hour
	INVOKE putch ,':'					 ;display a colon
	INVOKE intasc, ADDR strMinute, wMinute2 ;convert 2nd minute to ascii for display
	INVOKE putstring,ADDR strMinute			;display 2nd minute
	INVOKE putch ,':'						;display a colon
	INVOKE intasc, ADDR strSecond, wSecond2	;convert 2nd second to ascii for display
	INVOKE putstring,ADDR strSecond			;display 2nd second
	INVOKE putch ,':'						;display a colon
	INVOKE intasc, ADDR strMillisecs, wMillisecs2;convert 2nd millisecond to ascii
	INVOKE putstring,ADDR strMillisecs		;displaydisplay 2nd millisecond
	tab										;tab over
	
difference:
		mov ax, wMillisecs2   ;move to ax to prepare for subtraction
		sub ax, wMillisecs	  ;subtract the first millisecondtime call from the second
		jc subtract_MilSecsCarry ;if the bottom is greater than the top borrow from seconds
		mov wMillisecsDiff,ax	 ;store difference for later use
second:
		mov ax, wSecond2		;move to ax to prepare for subtraction 
		sub ax, wSecond			;subtract the first second from the 2nd second
		jc subtract_SecsCarry	;if bottom is greater than the top borrow from minutes
		mov wSecondDiff,ax		;store difference for later use
minute:
		mov ax, wMinute2		;move to ax to prepare for subtraction
		sub ax, wMinute			;subtract first minute from the second minute
		jc sbtract_MinutesCarry ;if bottom is greater than top borrow from hour
		mov wMinuteDiff, ax     ;store difference for later use
hour:   
		mov ax, wHour2			;move to ax to prepare for subtraction
		sub ax, wHour			;subtract first hour from second
		jc subtract_HourCarry	;if first greater than second borrow from day
		mov wHourDiff,ax		;store difference for later use
		
		mov ax,wDay2			;move to ax to prepare for subtraction
		sub ax,wDay				;subtract first day from second day
		mov wDayDiff,ax			;store difference for later use
		
		mov ax, wDayOfWeek2		;move to ax to prepare for subtraction
		sub ax, wDayOfWeek		;subtract second day of the week from the first
		mov wDayOfWeekDiff,ax   ;store difference for later use
		
		mov ax,wMonth2			;move to ax to prepare for subtraction
		sub ax,wMonth			;subtract first month from the second
		mov wMonthDiff,ax		;store difference for later use
		
		mov ax, wYear2			;move to ax to prepare for subtraction
		sub ax, wYear			;subtract first year from the second
		mov wDayOfWeekDiff,ax   ;store difference for later use
		jmp displayDifference	;after all subtraction has been sucessfully completed
								;jump to display the difference
								
subtract_MilSecsCarry:
		cmp wSecond2,0     ;if seconds equal zero then there is nothing to borrow from
		je secondEqualZero ;so, jump and borrow one from the minutes
		sub wSecond2,1	   ;when borrow is possible take one away from seconds and
		add wMillisecs2,1000	;add one second worth to the milliseconds place
		jmp difference	   ;now attempt to find the difference again
		
subtract_SecsCarry:
		cmp wMinute2,0     ;if minutes equal zero then there is nothin to borrow form
		je  minuteEqualZero;so borrow from the hours 
		sub wMinute2,1     ;when borrow is possible take one away from the minutes place 
		add wSecond2,60	   ;and add a minutes worth of seconds to the seconds place 
		jmp second		   ;now attemt to find the seconds difference again
		
sbtract_MinutesCarry:
		sub wHour2,1	   ;borrow one from the hours place and
		add wMinute2,60	   ;and add an hours worth of minutes to the minutes place
		jmp minute		   ;atempt to find the minutes difference again
subtract_HourCarry:
		sub wDay2,1		   ;borrow one from the days place and
		add wHour2,24	   ;add a days worth of hours to the hours place
		jmp hour		   ;attempt to find the hours difference again
		
secondEqualZero:
		cmp wMinute2,0	   ;if minutes equal zero
		je  minuteEqualZero;borrow one from the hours place
		sub wMinute2,1	   ;else subtract one from minutes and
		add wSecond2,60	   ;add a minutes worth of seconds to the seconds place
		jmp difference	   ;repeat trying to find the difference
minuteEqualZero:
		
		sub wHour2,1	;subtract one from the hours place
		add wMinute2,60	;and add an hours worth of minutes to the minutes place
		jmp difference	;repeat trying to find the difference
		
displayDifference:
		puts strDifference	;display "Difference: "
		INVOKE intasc, ADDR strYear, wYearDiff ;convert the year difference for display
		INVOKE putstring,ADDR strYear		   ;display the year difference
		INVOKE putch ,':'					   ;display a colon
		INVOKE intasc, ADDR strMonth, wMonthDiff ;convert the month difference for display
		INVOKE putstring,ADDR strMonth		   ;display the month difference
		INVOKE putch ,':'					   ;display a colon
		INVOKE intasc, ADDR strDayOfTheWeek, wDayOfWeekDiff ;convert the DOWD for display
		INVOKE putstring,ADDR strDayOfTheWeek  ;display the day of the week			
		INVOKE putch ,':'					   ;display a colon
		INVOKE intasc, ADDR strDay, wDayDiff   ;convert the day difference for display
		INVOKE putstring,ADDR strDay		   ;display the day
		INVOKE putch ,':'					   ;display a colon
		INVOKE intasc, ADDR strHour, wHourDiff ;convert the hour difference for display
		INVOKE putstring,ADDR strHour		   ;display the hour difference
		INVOKE putch ,':'					   ;display a colon
		INVOKE intasc, ADDR strMinute, wMinutediff ;convert the minute diff for display
		INVOKE putstring,ADDR strMinute		   ;display minute difference
		INVOKE putch ,':'					   ;display a colon
		INVOKE intasc, ADDR strSecond, wSecondDiff ;convert the second diff for display
		INVOKE putstring,ADDR strSecond		   ;display the second difference
		INVOKE putch ,':'					   ;display a colon
		INVOKE intasc, ADDR strMillisecs, wMillisecsDiff ;convert the millisecond difference 
		INVOKE putstring,ADDR strMillisecs				 ;display the millisecond difference	
		newLine								   ;add a new line
		puts strElapsed 		;display "The Elaspsed time in milliseconds is: "
		movsx eax,wHourDiff ;move to eax to prepare for multiplication 
		mov Ebx,3600000		;number of milliseconds in one hour
		imul ebx			;multiply EBX by EAX 
		mov iDiff,eax		;store product in iDiff
		
		movsx eax,wMinuteDiff	;move to eax to prepare for multiplication
		mov ebx, 60000			;number of milliseconds in one minute
		imul ebx				;multiply ebx by eax
		add iDiff,eax			;add the product to iDiff
		
		movsx eax, wSecondDiff	;move to eax to prepare for multiplication
		mov ebx,1000			;number of milliseconds in one second
		imul ebx				;multiply eax by ebx
		add iDiff, eax			;add product to iDiff
		
		movsx eax,wMillisecsDiff ;sign extend turn word into dWord
		add iDiff,eax			 ;add number milliseconds to iDiff
		
		INVOKE intasc32Comma,ADDR strDiff,iDiff ;convert with commas for display
		puts strDiff			 ;display the difference in milliseconds
		newLine					 ;display a blank line
		puts strPrompt1                   ;display "Enter Earlier Time"
		puts strPrompt3	                  ;display "Hours: "
		INVOKE getstring, ADDR strHour, 2 ; get the hours from the user
		cmp strHour,0					  ;if usr only hits enter key
		je finished						  ;program closes
		INVOKE ascint32, ADDR strHour	  ;convert to number to perform calculations with
		mov wHour,ax					  ;store number in wHour
		
		puts strPrompt4					  ;display "Minute: "
		gets strMinute,2                  ;get the minutes from the user
		cmp strMinute,0					  ;if the user only presses enter
		je finished						  ;program closes
		INVOKE ascint32, ADDR strMinute   ;convert to number to perform calculations with
		mov wMinute,ax					  ;store number in wMinute
		
		puts strPrompt5					  ;display "Seconds: "
		gets strSecond,2				  ;get seconds from the user
		cmp strSecond,0					  ;if the user only presses enter
		je finished						  ;program closes
		INVOKE ascint32, ADDR strSecond	  ;convert to a number to perform calculations with
		mov wSecond,ax					  ;store the number in wSeconds
		
		puts strPrompt6					  ;display "Milliseconds: "
		gets strMillisecs,3				  ;get milliseconds from the user
		cmp strMillisecs,0				  ;if the user only process enter
		je finished						  ;program closes
		INVOKE ascint32, ADDR strMillisecs;convert to a number to perform calculations with
		mov wMillisecs,ax				  ;store that number in wMillisecs
		
		newLine							  ;add a new line
		
		puts strPrompt2                   ;display "Enter Later Time"
		puts strPrompt3					  ;display "Hours: "
		gets strHour,2					  ;get the hours from the user
		cmp strHour,0					  ;if the user only presses enter
		je finished						  ;program closes
		INVOKE ascint32, ADDR strHour	  ;convert to number to perform calculations with
		mov wHour2,ax					  ;store number in wHour2
		
		puts strPrompt4					  ;display "Minute: "
		gets strMinute,2                  ;get the minutes from the user
		cmp strMinute,0					  ;if the user only presses enter
		je finished						  ;program closes
		INVOKE ascint32, ADDR strMinute   ;convert to number to perform calculations with
		mov wMinute2,ax					  ;store number in wMinute2
		
		puts strPrompt5					  ;display "Seconds: "
		gets strSecond,2				  ;get seconds from the user
		cmp strSecond,0					  ;if the user only presses enter
		je finished						  ;program closes
		INVOKE ascint32, ADDR strSecond	  ;convert to a number to perform calculations with
		mov wSecond2,ax					  ;store the number in wSeconds2
		
		puts strPrompt6					  ;display "Milliseconds: "
		gets strMillisecs,3				  ;get milliseconds from the user
		cmp strMillisecs,0				  ;if the user only process enter
		je finished						  ;program closes
		INVOKE ascint32, ADDR strMillisecs;convert to a number to perform calculations with
		mov wMillisecs2,ax				  ;store that number in wMillisecs2
		newLine							  ;add a new line
		jmp displayTime					  ;jump to line 132
		
finished:	;when user presses enter program terminates normally
	INVOKE ExitProcess,0				;terminate "normally" the program
	PUBLIC _start						;always end a program with this line
	
	
	END								;The very LAST line in your program. Terminate assembly
	
	
	
	
	
	
	