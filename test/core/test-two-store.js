const gb = require("../gbtest")(__filename);

test("two-store", () => {
  gb.run();

  expect(gb.depth).toBe(0);
  expect(gb.memory[0x8501]).toBe(0xcd);
  expect(gb.memory[0x8502]).toBe(0xab);
  expect(gb.memory[0x8503]).toBe(0x34);
  expect(gb.memory[0x8504]).toBe(0x12);
  expect(gb.memory[0x8505]).toBe(0xdc);
  expect(gb.memory[0x8506]).toBe(0xfe);
  expect(gb.memory[0x8507]).toBe(0x65);
  expect(gb.memory[0x8508]).toBe(0x87);
});
