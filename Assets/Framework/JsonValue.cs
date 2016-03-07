using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public enum ValueType
    {
       nullValue = 0, 
       intValue,      
       uintValue,     
       realValue,     
       stringValue,   
       booleanValue,  
       arrayValue,    
       objectValue    
    };


[StructLayout(LayoutKind.Explicit, Pack = 1)]
internal class ValueHolder
{
	[FieldOffset(0)]public string _string;
	[FieldOffset(0)]public int _int;
	[FieldOffset(0)]public uint _uint;
	[FieldOffset(0)]public double _double;
	[FieldOffset(0)]public bool _bool;
}

public class JsonValue {

	private ValueHolder _value = new ValueHolder();
	private ValueType _type;


	private List<string> _members;
	private Dictionary<string, JsonValue> _objectValues;

	public JsonValue() {
		_value._int = 0;
		_type = ValueType.nullValue;
	}
	public JsonValue( int i ) {
		_value._int = i;
		_type = ValueType.intValue;
	}
	public JsonValue( uint i ) {
		_value._uint = i;
		_type = ValueType.uintValue;
	}
	public JsonValue( double i ) {
		_value._double = i;
		_type = ValueType.realValue;
	}
	public JsonValue( string s ) {
		_value._string = s;
		_type = ValueType.stringValue;
	}
	public JsonValue( bool b ) {
		_value._bool = b;
		_type = ValueType.booleanValue;
	}
	public JsonValue( ValueType type ) 
	{
		_value._int = 0;
		_type = type;
	}

	public bool isNull() { 
		return _type == ValueType.nullValue;
	}
	public bool isBool() { 
		return _type == ValueType.booleanValue;
	}
	public bool isInt() {
		return _type == ValueType.intValue;
	}
	public bool isUInt() {
		return _type == ValueType.uintValue;
	}
	public bool isIntegral() {
		return _type == ValueType.intValue || _type == ValueType.uintValue;
	}
	public bool isDouble() {
		return _type == ValueType.realValue;
	}
	public bool isNumeric() {
		return _type == ValueType.intValue || _type == ValueType.uintValue || _type == ValueType.realValue;
	}
	public bool isString() {
		return _type == ValueType.stringValue;
	}
	public bool isArray() {
		return _type == ValueType.arrayValue;
	}
	public bool isObject() {
		return _type == ValueType.objectValue;
	}

	public int asInt(){
		switch (_type) {
		case ValueType.intValue: return _value._int;
		case ValueType.uintValue: return (int) _value._uint;
		default:
			throw new System.InvalidCastException("JsonValue not numerical");
		}
	}

	public bool asBool() {
		switch (_type) {
		case ValueType.booleanValue: return _value._bool;
		case ValueType.intValue: return (_value._int != 0);
		default:
			throw new System.InvalidCastException("JsonValue not bool");
		}
	}

	public string asString() {
		switch (_type) {
		case ValueType.stringValue: return _value._string;
		default:
			throw new System.InvalidCastException("JsonValue not string");
		}
	}
}
