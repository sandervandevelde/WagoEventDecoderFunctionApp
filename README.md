# WagoEventDecoderFunctionApp

Example function to decode EventGridTopic CloudEvent Schema V1.0 for Microsoft Fabric usage.

## Introduction

The Microsoft Fabric (enhanced) eventstream cannot cope with messages formatted in the CloudEvent Schema V1.0 format.

Using this function CloudEvents sent by an EventGrid Topic via a webhook subscription, the base64 data is made available as JSON string.

## Flow

This function ingests telemetry from an EventGrid Topic webhook endpoint subscription

The messages data field is sent towards a custom source in a Microsoft Fabric Eventstream using the EventHub credentials

## Environment variable

The Microsoft Fabric Eventstream custom source EventHub credentials must be made available in an Environment variable.

Please provide the connectionstring:

```
CustomAppEndpoint
```

Add this variable as an Azure Function app setting name-value.

## License

This code is licensed under MIT.