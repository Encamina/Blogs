import 'dart:convert' as convert;

import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'package:http/http.dart' as http;
import 'package:jwt_decoder/jwt_decoder.dart';
import 'package:uuid/uuid.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Flutter Demo',
      theme: ThemeData(
        primarySwatch: Colors.blue,
      ),
      home: const MyHomePage(title: 'Flutter Demo Home Page'),
    );
  }
}

class MyHomePage extends StatefulWidget {
  const MyHomePage({super.key, required this.title});
  final String title;

  @override
  State<MyHomePage> createState() => _MyHomePageState();
}

class _MyHomePageState extends State<MyHomePage> {
  static const platform = MethodChannel('com.example.flutter_azure_calling_ui/calling');
  late TextEditingController _groupCallIdController;
  late TextEditingController _userIdController;
  late TextEditingController _userNameController;
  late String _userToken;

  @override
  void initState() {
    super.initState();
    _groupCallIdController = TextEditingController();
    _userIdController = TextEditingController();
    _userNameController = TextEditingController();
  }

  @override
  void dispose() {
    _groupCallIdController.dispose();
    _userIdController.dispose();
    _userNameController.dispose();
    super.dispose();
  }

  void _getToken() async {
    var response = await http.get(Uri.parse('https://acs-authentication-service.azurewebsites.net/api/Function'));
    if (response.statusCode != 200) {
      return;
    }

    var jsonResponse = convert.jsonDecode(response.body) as Map<String, dynamic>;
    var token = jsonResponse['token'];

    _userToken = token;
    Map<String, dynamic> decodedToken = JwtDecoder.decode(token);
    _userIdController.text = decodedToken["skypeid"];
  }

  void _joinGroup() async {
    final bool result = await platform.invokeMethod('startCall', {"groupCallId": _groupCallIdController.text, "displayName": _userNameController.text, "userToken": _userToken});
    if (kDebugMode) {
      print("The call result is $result");
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(widget.title),
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Center(
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: <Widget>[
              const SizedBox(height: 10),
              TextButton(onPressed: _getToken, child: const Text('Get token')),
              const SizedBox(height: 10),
              TextField(
                controller: _userIdController,
                decoration: const InputDecoration(labelText: 'User ID'),
                readOnly: true,
              ),
              const SizedBox(height: 10),
              TextField(
                controller: _userNameController,
                decoration: const InputDecoration(labelText: 'User name'),
              ),
              Row(
                children: [
                  Expanded(
                    child: TextField(
                      controller: _groupCallIdController,
                      decoration: const InputDecoration(labelText: 'Group call ID'),
                    ),
                  ),
                  const SizedBox(width: 5),
                  IconButton(
                    icon: const Icon(Icons.refresh_rounded),
                    onPressed: () {
                      var uuid = const Uuid();
                      setState(() {
                        _groupCallIdController.text = uuid.v1();
                      });
                    },
                  ),
                ],
              ),
              const SizedBox(height: 10),
              TextButton(onPressed: _joinGroup, child: const Text('Join group call')),
            ],
          ),
        ),
      ),
    );
  }
}
