﻿/*
    Copyright 2016 © Samantha Rachel Belnavis
    Licensed under the GNU General Public License, Version 3.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at
        http://www.gnu.org/licenses/gpl.html
    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for specific language governing permissions and
    limitations under the License.
    Program Created by: 	Samantha Rachel Belnavis
	Date Created:			December 12, 2016
	Date Last Modified: 	December 20, 2016
    File Name: 				MainProgram.cs
    File Description: 		Main GUI controller for Sojourner
*/

using System;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using NUnit.Framework;
using Gtk;
using GLib;
using System.IO;

//Suppress "unused private variable" / "unused class" / "unused variable" / "declared but never used"
#pragma warning disable CS0414, CS0169, CS0219, CS0168

namespace SojournerGUI
{
	
	// TCP Stream Class
	public class CreateNewSocket
	{
		delegate void Delegate(NetworkStream netStream);

		public System.Net.Sockets.Socket MainConnection()
		{
			System.Net.Sockets.Socket clientSocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream,
																				   ProtocolType.Tcp);

			return clientSocket;
		}
	}

	/*
	// Streamwriter Class
	public class CreateStreamWriter
	{
		delegate void Delegate(StreamWriter streamWriter);

		public StreamWriter writeCmd()
		{
			var streamInstance = new CreateTcpStream();
			streamInstance.MainConnection();

			var netStreamStore = streamInstance.MainConnection();

			StreamWriter cmdStream = new StreamWriter(netStreamStore);

			return cmdStream;
		}
	}*/


	// GUI Class
	public class MainGui
	{

		// Get Key states and write to network stream
		[GLib.ConnectBefore()]
		public static void KeyPressEvent(object sender, Gtk.KeyPressEventArgs args)
		{
			
			// Storage for key pressed
			var keyOut = args.Event.Key;

			//New Command Strings
			string cmdMovFwd = "mov_fwd";
			string cmdMovRev = "mov_rev";
			string cmdTurnLeft = "turn_left";
			string cmdTurnRight = "turn_right";


			// -------------- Key definitions --------------

			//If key pressed / held is "w" or "W"
			if (args.Event.KeyValue == 0x057 || args.Event.KeyValue == 0x077)
			{
				Console.WriteLine("Keypress: {0}", args.Event.Key);
				byte[] cmdOut = Encoding.UTF8.GetBytes(cmdMovFwd);
				//netStream.W(cmdOut, cmdOut.Length);
			}

			//If key pressed / held is "a" or "A"
			if (args.Event.KeyValue == 0x041 || args.Event.KeyValue == 0x061)
			{
				Console.WriteLine("Keypress: {0}", args.Event.Key);
			}

			//If key pressed / held is "s" or "S"
			if (args.Event.KeyValue == 0x053 || args.Event.KeyValue == 0x073)
			{
				Console.WriteLine("Keypress: {0}", args.Event.Key);
			}

			//If key pressed / held is "d" or "D"
			if (args.Event.KeyValue == 0x044 || args.Event.KeyValue == 0x064)
			{
				Console.WriteLine("Keypress: {0}", args.Event.Key);
			}

			//Extra Peripherals

			//If key pressed is "c" or "C"
			if (args.Event.KeyValue == 0x043 || args.Event.KeyValue == 0x063)
			{
				Console.WriteLine("Keypress: {0}", args.Event.Key);
				//@TODO Camera control code
			}

			//If key pressed is "l" or "L"
			if (args.Event.KeyValue == 0x04c || args.Event.KeyValue == 0x06c)
			{

			}
		}



		// GUI Specific Events

		//Runs when the window is closed
		static void delete_event(object obj, DeleteEventArgs args)
		{
			Gtk.Application.Quit();
		}

		// Runs when the "test button" button is pressed
		static void test(object obj, EventArgs args)
		{
			/*
			var streamWriterInstance = new CreateStreamWriter();
			streamWriterInstance.writeCmd();

			var writeCmd = streamWriterInstance.writeCmd();*/

			string server = "192.168.0.4";
			int port = 8888;

			var connSocket = new CreateNewSocket();
			connSocket.MainConnection();

			var clientSocket = connSocket.MainConnection();

			System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(server);
			System.Net.IPEndPoint destination = new System.Net.IPEndPoint(ipAdd, port);
			clientSocket.Connect(destination);

			Console.WriteLine("Connected to Server");

			byte[] dataOutput = System.Text.Encoding.UTF8.GetBytes("test message");
			clientSocket.Send(dataOutput);
			clientSocket.Close();

			                          

			Console.WriteLine("button was pressed");
		}


		// Main Method
		public static void Main(string[] args)
		{

			//Server Connection Details

			string server = "192.168.0.4";
			int port = 8888;

			//Establish connection
			var connSocket = new CreateNewSocket();
			connSocket.MainConnection();

			var clientSocket = connSocket.MainConnection();

			System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(server);
			System.Net.IPEndPoint destination = new System.Net.IPEndPoint(ipAdd, port);
			clientSocket.Connect(destination);

			Gtk.Application.Init();

			//New Button
			Gtk.Button btn = new Gtk.Button("Test Button");

			// Run something when a button is pressed 
			btn.Clicked += new EventHandler(test);
			
			MainWindow win = new MainWindow();
			win.KeyPressEvent += new Gtk.KeyPressEventHandler(MainGui.KeyPressEvent);

			//Add buttons to new window
			win.Add(btn);
			win.ShowAll();

			Gtk.Application.Run();
		}
	}
}
