MaskAttribute
=============

Simple custom attribute implementation for masking an input.

Usage
=====

The attribute has two parameters, the first one is a jQuery selector for the input, the second one, is the mask.

public clas MyModel {
    [Mask("#MaskedAttribute", "AAA 999")]
    public object MaskedAttribute { get; set; }
}

Character Definitions
=====================

- '9': [0-9]
- 'a': [a-z]
- 'A': [A-Z]
- '*': [A-Za-z0-9]
