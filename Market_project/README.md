Objective

Write a program that models the following market scenario:

    Sellers have a limited number of products available per unit of time, which they offer on the open market at prices determined by:
        The cost of producing the product.
        Inflation (as the value of money decreases over time) and taxes.
        The seller's margin (how much profit they want to make on the product).

    The goal of the seller is to achieve the highest possible profit.

    Buyers have needs, principles, and money. They observe the product offers on the market. Their behavior is governed by the following rules:
        They want to purchase certain products and monitor their prices, but they do not have to buy them immediately.
        They are aware of the rate of inflation.
        Their willingness to purchase a product decreases as the price of the product increases, regardless of whether the price rise is due to inflation or the seller's margin.

    The Central Bank monitors the increase in product prices and market turnover. It sets the current level of inflation. The bank aims to maintain steady tax revenues, calculated as the product of inflation and turnover at the given inflation rate.

Patterns to Implement:

    Use the Visitor Pattern to update information about products for sellers and to adjust buyers' parameters. 
    Use the Observer Pattern for passive observation of changes:
        Sellers and Buyers observe the Central Bank to learn about the current level of inflation.
        Buyers observe the Sellers' offers and may respond to them, but they are not required to do so.

Note

The project should also include appropriate unit tests for the implemented functionality