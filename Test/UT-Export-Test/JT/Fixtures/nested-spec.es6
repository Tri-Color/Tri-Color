describe("top describe", function () {
    'use strict';

    var someVariable;

    beforeEach(module(Tiger.coreModule.name));

    beforeEach(inject(function ($rootScope) {
        
    }));

	it("top it 1", function () {});

	it("top it 2", function () {});

    describe("describe 1", function () {
        it("it 1.1", function () {
            
        });

        it("it 1.2", () => {

        });

        describe("describe 1.3", function () {
            it("it 1.3.1", function () {
                
            });

            it("it 1.3.2", () => {

            });
        });
    });

    describe("describe 2", () => {
        it("it 2.1", function () {
            
        });

        it("it 2.2", () => {

        });
    });
});