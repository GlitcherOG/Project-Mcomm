# Mcomm Project Server (SSX3 Online Server)
For Live Server Info: http://ssxor.org
## Status
General  
• Challenging Player: ✅  
• Quick Play:  

Accounts  
  
• Authentication & Account Creation: ✅  
• Persona creation & Persona deletion: ✅  
• Account Management: ✅  
• Username & Password recovery:  

Rooms  

• Create & join rooms ✅  
• Text Chat: ✅  
• Voice Chat:  
• Kick player:  
• Password-locked rooms: ✅

Buddies  

• Searching for buddies: ✅  
• Add as Buddy: ✅ (Disabled currently, Show as blocked when leaving and rejoining)  
• Removing buddies:  
• Blocking buddies:  
• Online Status: ✅  
• Send & receive messages: ✅  
• View profile: ✅  
• Pressence info: ✅

Statistics  

• Leaderboards: ✅ (Overall score missing)  
• Rider Stats:  
• Ranked Play: ✅  

News  

• News: ✅

## Todo
- Fix Bug where users are blocked on buddy list when logging in
- !last to list stats for last game
- Add Remove Buddy
- Send Online Details before displaying Search
- Patch Search so it gets offline personas
- Find out Stat Struct
- Add lost user
- Fix Quick Match So it Works
- Persona Generator for Duplicate Personas
- Fix Having to DQUE to leave room (Probably due to an issue with vc)
- Mcomm Buddy for server commands
- Fix Proper Passwords
- Fix Title on rooms as it doesnt seem to be appearing
- Tunneling Service
- PAL and NTSC Prefix for accounts

# Todo Discord Bot
- Discord bot able to send messages in game and to rooms
- Set Rooms to Global
- Discord Bot Fix Kick Messages
- Link Ingame Account To Discord Account

## Setting Up Server (Not Required Unless hosting a server)
1. Run Server to generate config file and nessasary files
2. Port forward all three ports found in the config file to your server ListenerPort (11000), GamePort (10901), BuddyPort (13505)
3. Edit the config file to contain the IP Address for your server

## Setting Up Client PCSX2
1. Using DNAS Patcher 21 bypass (Found on the ssx modding server) the DNAS Checks using mode 1 and 3 to generate an iso to bypass dnas check
2. (If using pnatch for server address go to step 5 and leave dns1 on auto) In Emulator go to network settings swap DNS1 Address to Internal
3. Go to Internal DNS
4. Add a new entry for URL ps2ssx04.ea.com to go to the ip address of the server and ensure its enabled
5. Configure the Emulator to Use PCAP Switched or PCAP Bridged (See Below)
6. Port Forward UDP 3658-3659 for Gameplay, UDP 6000-6001 for Voice Chat to the emulator PS2 Address you set for PCAP on your router (See FAQ for more details)
7. Setup network configration in game. When creating the settings to use are Not Required, Auto, Auto.

## PCAP Switched (Ethernet) / PCAP Bridged (WiFi)
Download NPCAP  
Note: PCAP Switched is the most reliable option for connecting with others.  
Note: PCAP options treat PCSX2 as its own device on your network. So, we need to create an address for it (PS2 Address)  
1. In PCSX2, open Settings / Network & HDD.
2. Under Ethernet Device Type, choose the PCAP option that fits your connection type.
3. Enable Intercept DHCP & find the PS2 Address Box.
4. Enter the first 3 sections of your default gateway address (XXX.XXX.X.)
5. For the final section, enter any number 100 - 250
Note: Choosing a high number will give you a better chance at avoiding conflict with existing devices.

## Upnp (Alternative to Port forwarding)
[Download PortMapper](https://github.com/kaklakariada/portmapper)
Note: This may work as an aternative to port forwarding or may fail depending on router settings
1. On the Router Page Click Connect
2. If it fails to connect go to port mapper settings and try a different NPnP Library (Ignoring Dummy Router) and go to step 1
3. Once Connected Create a Port Mapping Preset
4. Name it SSX 3 and Add Port Range UDP 3658-3659 for Gameplay, UDP 6000-6001 for Voice Chat
5. Untick Use Local Host and Put in PS2 Address
6. Save and select the preset and click use
7. If it appears in the port mappings at the top its worked otherwise you may need to review router settings

## Prevent Windows from messing with your connection (PCSX2 Only)  
__Firewall Exception for PCSX2__  
1. Control Panel -> System and Security -> Windows Defender Firewall  
2. Allow an app or feature through Windows Defender Firewall  
3. Allow another app... -> Browse... -> PCSX2 PS2 Emulator.exe -> Add  

__Inbound Rules__  
1. Control Panel -> System and Security -> Windows Defender Firewall  
2. Advanced Settings -> Inbound Rules -> New Rule  
3. Rule Type: Custom -> Program: This Program Path -> Browse... -> PCSX2 PS2 Emulator.exe  
4. Protocol and Ports -> Protocol Type: UDP  
5. Name: PCSX2 UDP -> Finish  
6. Repeat but for TCP (select Protocol Type: TCP)  

## FAQ
- PAL and NTSC Crossplay doesn't work.
- Without Port Forwarding for UDP 3658-3659 you can still play however it will be limited. You will only be able to challange people who have port forwarded and wont be able to receive challanges
- You wont be able to challange people who havent port forwarded UDP 3658-3659
- Gateway address is the address for your router on the local network (e.g. 192.168.0.1) can be found out using the cmd and ipconfig command
- 2 Players on the same network can play together when connected to the same network.
- User has declined your challange. It is a bug that happens with the game however if it soon doesnt launch into the challange check port forwarding

## Donate
[![Donate](https://www.paypalobjects.com/en_AU/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/donate/?business=VT6TG8KKZM98E&no_recurring=0&currency_code=AUD)
