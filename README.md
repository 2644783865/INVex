# INVex ORM

Попытка в свою ORM, просто для развития.

# TODO:
* ORM
* Expressions
* Queries
* Commands
* Actions
* Scenarios

# ORM USAGE:
##### Untyped query example
----------
Example of a simple untyped query of ChatMessage objects:
```csharp
ObjectQuery messagesQr = new ObjectQuery("ChatMessage")
{
	ReturnedAttributes = new List<IPathElement>
	{
		new AStep("Name"),
		new AStep("ChatId")
	}
};
List<IObjectInstance> messages = messagesQr.Execute();
```
The query above returns all of `ChatMessage` objects exist in a DB, and only maps `Name`, `ChatId` and `PK`.

The query is equivalent to a SQL statement below:
```sql
SELECT [ChatMessageId],[Name],[ChatId] FROM T_ChatMessages
```
`ChatMessage` XML model example:
```xml
<ObjectModel Name="ChatMessage" PrimaryKey="Guid">
  <Fields>
    <Property Name="Guid" ValueType="string" />
    <Property Name="Name" ValueType="string" />
    <Property Name="Message" ValueType="string" />
    <Property Name="Time" ValueType="datetime" />
    <Property Name="ChatId" ValueType="string" />
  </Fields>
  <Mapping Prefix="" Table="T_ChatMessages">
    <Map Attribute="Guid" Source="Guid" />
    <Map Attribute="Name" Source="Name" />
    <Map Attribute="Message" Source="Message" />
    <Map Attribute="Time" Source="Time" />
    <Map Attribute="ChatId" Source="ChatId" />
  </Mapping>
</ObjectModel>
```
### Typed query example
----------
First, we need a class inheritin ObjectIntsance