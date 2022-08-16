// const splitOnTags = (str) =>
//   str.split(/(<\/?[^>]+>)/g).filter((line) => line.trim() !== "");
// const isTag = (str) => /<[^>!]+>/.test(str);
// const isXMLDeclaration = (str) => /<\?[^?>]+\?>/.test(str);
// const isClosingTag = (str) => /<\/+[^>]+>/.test(str);
// const isSelfClosingTag = (str) => /<[^>]+\/>/.test(str);
// const isOpeningTag = (str) =>
//   isTag(str) &&
//   !isClosingTag(str) &&
//   !isSelfClosingTag(str) &&
//   !isXMLDeclaration(str);

// var res = "";
// var cache;

// function repeat(str, num) {
//   if (typeof str !== "string") {
//     throw new TypeError("expected a string");
//   }

//   // cover common, quick use cases
//   if (num === 1) return str;
//   if (num === 2) return str + str;

//   var max = str.length * num;
//   if (cache !== str || typeof cache === "undefined") {
//     cache = str;
//     res = "";
//   } else if (res.length >= max) {
//     return res.substr(0, max);
//   }

//   while (max > res.length && num > 1) {
//     if (num & 1) {
//       res += str;
//     }

//     num >>= 1;
//     str += str;
//   }

//   res += str;
//   res = res.substr(0, max);
//   return res;
// }

// function xmlFormatter(xml, indent) {
//   let depth = 0;
//   indent = indent || "    ";
//   let ignoreMode = false;
//   let deferred = [];

//   return splitOnTags(xml)
//     .map((item) => {
//       if (item.trim().startsWith("<![CDATA[")) {
//         ignoreMode = true;
//       }
//       if (item.trim().endsWith("]]>")) {
//         ignoreMode = false;
//         deferred.push(item);
//         let cdataBlock = deferred.join("");
//         deferred = [];
//         return cdataBlock;
//       }
//       if (ignoreMode) {
//         deferred.push(item);
//         return null;
//       }

//       // removes any pre-existing whitespace chars at the end or beginning of the item
//       item = item.replace(/^\s+|\s+$/g, "");
//       if (isClosingTag(item)) {
//         depth--;
//       }

//       const line = repeat(indent, depth) + item;

//       if (isOpeningTag(item)) {
//         depth++;
//       }

//       return line;
//     })
//     .filter((c) => c)
//     .join("\n");
// }

// export function xmlBeautify(xml) {
//   var result = xmlFormatter(xml, null);

//   var result = xml.replace("\r", "").replace("\n", "<br>");
//   result = result.replace(" ", "&nbsp;");

//   return result;
// }
