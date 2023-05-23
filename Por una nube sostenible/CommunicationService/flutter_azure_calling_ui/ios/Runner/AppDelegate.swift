import UIKit
import Flutter
import AzureCommunicationCalling
import AzureCommunicationUICalling
import Foundation

@UIApplicationMain
@objc class AppDelegate: FlutterAppDelegate {
    
    private var callComposite: CallComposite?
    
    override func application(_ application: UIApplication, didFinishLaunchingWithOptions launchOptions: [UIApplication.LaunchOptionsKey: Any]?) -> Bool {
        let controller : FlutterViewController = window?.rootViewController as! FlutterViewController
        let methodChannel = FlutterMethodChannel(name: "com.example.flutter_azure_calling_ui/calling",
                                                  binaryMessenger: controller.binaryMessenger)
        methodChannel.setMethodCallHandler({
          (call: FlutterMethodCall, result: @escaping FlutterResult) -> Void in
            switch call.method {
            case "startCall":
                guard let args = call.arguments as? [String: String] else {return}
                let groupCallId = args["groupCallId"]!
                let displayName = args["displayName"]!
                let token = args["userToken"]!
                self.startCallComposite(groupCallId: groupCallId, displayName: displayName, userToken: token)
                result(true)
            default:
                result(FlutterMethodNotImplemented)
            }
        })
    
        GeneratedPluginRegistrant.register(with: self)
        return super.application(application, didFinishLaunchingWithOptions: launchOptions)
    }
    
    private func startCallComposite(groupCallId: String, displayName: String, userToken: String) {
        let callCompositeOptions = CallCompositeOptions();
        callComposite = CallComposite(withOptions: callCompositeOptions);
        let communicationTokenCredential = try! CommunicationTokenCredential(token: userToken);
        let remoteOptions = RemoteOptions(for: .groupCall(groupId: UUID(uuidString: groupCallId)!),
        credential: communicationTokenCredential,
        displayName: displayName)

        callComposite?.launch(remoteOptions: remoteOptions)
    }
}

