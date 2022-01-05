# Stark
Stark is a computer science algorithms library. It is designed to build new algorithms on top of exisiting computer science algorithms.

[![Build Status](https://travis-ci.org/Blackbird-tech/Stark.svg?branch=master)](https://travis-ci.org/Blackbird-tech/Stark)

## Features:

+ *Blackbird.Stark.Math.BigRational* - BigInteger with floating point value for really huge calculations. Precision might be adjusted, default value is 512 symbols after delimiter. Might be helpful for calculators development, etc.

+ *Blackbird.Stark.Ranges.Range* - class that helps you work with ranges, you can find out whether they are overlapping and you can merge Ranges to one. Also there is extension methods for *IEnumberable<Range>* that merges all ranges that overlap. Might be helpful during sports programming.
 
+ *Blackbird.Stark.Graph* - some APIs that can build graphs from char matrixes and search in graph using BFS. I used it for sports programming contests.

+ *Blackbird.Stark.Trees* - there is two implementation of tree - AVL Tree and BinarySearch Tree. Might be used for performance critical apps as data containers. More trees will be added later.

+ *Blackbird.Stark.Collections* - there is my LinkedList implementation, since .NET has only ArrayLists. There is also TreeDictionary that is wrapper around trees mentioned above. You can use tree as conatiner for a dictionary.

## Changelog

### 1.0.6

+ *AvlTree* balancing is fixed and ready to use. Previous implementation had issues with tree balancing. Now you can use it, itself and as a core for *TreeDictionary*. 

+ All trees can be printed to console with *PrintTree* method. Might be used for debug or visualisation. Each node has two indented children, first one is right child and the 2nd is left.

+ Small bug fixes

---
<p align="justify">
Crafted with love in Kyiv
</p>
| |
| :-: |
| Crafted with love in Kyiv |