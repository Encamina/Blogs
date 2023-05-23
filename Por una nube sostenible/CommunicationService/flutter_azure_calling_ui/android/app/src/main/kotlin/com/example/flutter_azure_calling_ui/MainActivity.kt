package com.example.flutter_azure_calling_ui

import com.azure.android.communication.common.CommunicationTokenCredential
import com.azure.android.communication.common.CommunicationTokenRefreshOptions
import com.azure.android.communication.ui.calling.CallCompositeBuilder
import com.azure.android.communication.ui.calling.models.CallCompositeGroupCallLocator
import com.azure.android.communication.ui.calling.models.CallCompositeJoinLocator
import com.azure.android.communication.ui.calling.models.CallCompositeRemoteOptions
import io.flutter.embedding.android.FlutterActivity
import io.flutter.embedding.engine.FlutterEngine
import io.flutter.plugin.common.MethodCall
import io.flutter.plugin.common.MethodChannel
import okhttp3.OkHttpClient
import okhttp3.Request
import okhttp3.Response
import org.json.JSONObject
import java.util.*


class MainActivity: FlutterActivity() {
    private val CHANNEL = "com.example.flutter_azure_calling_ui/calling"
    private lateinit var methodChannel: MethodChannel

    override fun configureFlutterEngine(flutterEngine: FlutterEngine) {
        super.configureFlutterEngine(flutterEngine)
        methodChannel = MethodChannel(flutterEngine.dartExecutor.binaryMessenger, CHANNEL)
        methodChannel.setMethodCallHandler { call: MethodCall, result: MethodChannel.Result ->
            if (call.method.equals("startCall")) {
                val groupCallId = call.argument<String>("groupCallId")
                val displayName = call.argument<String>("displayName")
                val userToken = call.argument<String>("userToken")
                startCallComposite(groupCallId!!, displayName!!, userToken!!)
                result.success(true)
            } else {
                result.notImplemented()
            }
        }
    }

    private fun startCallComposite(groupCallId: String, displayName: String, userToken: String) {
        val communicationTokenCredential =
            CommunicationTokenCredential(userToken)
        val locator: CallCompositeJoinLocator =
            CallCompositeGroupCallLocator(UUID.fromString(groupCallId))
        val remoteOptions =
            CallCompositeRemoteOptions(locator, communicationTokenCredential, displayName)
        val callComposite = CallCompositeBuilder().build()
        callComposite.launch(this, remoteOptions)
    }
}
