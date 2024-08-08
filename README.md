# WagoEventDecoderFunctionApp

Example function to decode EventGridTopic CloudEvent Schema V1.0 for Microsoft Fabric usage.

## Introduction

The Microsoft Fabric (enhanced) eventstream cannot cope with messages formatted in the CloudEvent Schema V1.0 format.

Using this function CloudEvents are sent by an EventGrid Topic via a webhook subscription, the base64 data is made available as JSON string.

See also those [blog post](https://sandervandevelde.wordpress.com/2024/08/02/microsoft-fabric-rti-derived-streams-in-the-real-time-hub/) for the full details.

## Flow

This function ingests telemetry from an EventGrid Topic webhook endpoint subscription

The messages data field is sent to a custom source in a Microsoft Fabric Eventstream using the EventHub credentials

## Environment variable

The Microsoft Fabric Eventstream custom source EventHub credentials must be made available in an Environment variable.

Please provide the connection string:

```
CustomAppEndpoint
```

Add this variable as an Azure Function app setting name-value.

## License

This code is licensed under MIT.
