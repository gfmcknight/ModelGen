﻿import assert = require('assert');
import Class4 from '../gen/TestSamples/Class4';
import Class5 from '../gen/TestSamples/Class5';
import AllListFieldsClass from '../gen/TestSamples/AllListFieldsClass';
import MapFieldsClass from '../gen/TestSamples/MapFieldsClass';

describe("Test Nested Models", () => {
    it("Test Simple Property", () => {
        var obj = new Class4({ Class1Prop: { MyNumber: 5, MyOtherProp: 3 } });
        assert.equal(obj.Class1Prop.MyNumber, 5);
        assert.equal(obj.Class1Prop.MyOtherProp, 3);
    });

    it("Test Model-Specific Property", () => {
        var obj = new Class5({ Class2Prop: { differentNumberName: 2 } });
        assert.equal(obj.Class2Prop.MyNumber, 2);
    });

    it("Test Model List", () => {
        var obj = new AllListFieldsClass({
            "ModelListProperty": [
                { "differentNumberName": 3 },
                { "differentNumberName": 6 }
            ]
        });

        assert.equal(obj.ModelListProperty[0].MyNumber, 3);
        assert.equal(obj.ModelListProperty[1].MyNumber, 6);
    });

    it("Test Model Dictionary", () => {
        var obj = new MapFieldsClass({
            StringToModel: {
                "myobj": { "differentNumberName": 14 }
            }
        });

        assert.equal(obj.StringToModel["myobj"].MyNumber, 14);
    });
});
