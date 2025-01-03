Set operations in LINQ provide a way to compare and combine collections based on the uniqueness or commonality of
elements. Here's a breakdown of the major set operations and how they work:

---

### **1\. Distinct and DistinctBy**

- **`Distinct`**: Removes duplicate elements from a collection based on their values.

  ```
  string[] words = { "the", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dog" };
  IEnumerable<string> distinctWords = words.Distinct();
  // Output: "the", "quick", "brown", "fox", "jumped", "over", "lazy", "dog"

  ```

- **`DistinctBy`**: Removes duplicates based on a specific property or key.

  ```
  string[] words = { "the", "quick", "brown", "fox", "jumped", "over", "the", "lazy", "dog" };
  IEnumerable<string> distinctByLength = words.DistinctBy(word => word.Length);
  // Output: "the", "quick", "jumped", "over" (one word per unique length)

  ```

---

### **2\. Except and ExceptBy**

- **`Except`**: Produces the set difference between two collections (elements in the first collection that are not in
  the second).

  ```
  string[] words1 = { "the", "quick", "brown", "fox" };
  string[] words2 = { "jumped", "over", "the", "lazy", "dog" };
  IEnumerable<string> difference = words1.Except(words2);
  // Output: "quick", "brown", "fox"

  ```

- **`ExceptBy`**: Filters elements from the first collection that do not match keys from the second.

  ```
  int[] excludeIds = { 1, 2, 3 };
  var result = students.ExceptBy(excludeIds, student => student.ID);
  // Students with IDs 4+ remain.

  ```

---

### **3\. Intersect and IntersectBy**

- **`Intersect`**: Produces the set intersection of two collections (elements that appear in both).

  ```
  string[] words1 = { "the", "quick", "brown", "fox" };
  string[] words2 = { "jumped", "over", "the", "lazy", "dog" };
  IEnumerable<string> intersection = words1.Intersect(words2);
  // Output: "the"

  ```

- **`IntersectBy`**: Matches elements from two collections based on a key selector.

  ```
  var matchingStudentsAndTeachers = students.IntersectBy(
      teachers.Select(t => t.ID),
      student => student.ID);
  // Output: Students who are also teachers based on ID.

  ```

---

### **4\. Union and UnionBy**

- **`Union`**: Combines two collections into one with unique elements.

  ```
  string[] words1 = { "the", "quick", "brown", "fox" };
  string[] words2 = { "jumped", "over", "the", "lazy", "dog" };
  IEnumerable<string> union = words1.Union(words2);
  // Output: "the", "quick", "brown", "fox", "jumped", "over", "lazy", "dog"

  ```

- **`UnionBy`**: Combines two collections into one, ensuring uniqueness based on a key selector.

  ```
  var allPeople = students
      .Select(s => (s.FirstName, s.LastName))
      .UnionBy(
          teachers.Select(t => (t.FirstName, t.LastName)),
          person => person.FirstName);
  // Output: Unique names from both students and teachers.

  ```

---

### **Key Points**

1.  **`Distinct` vs. `DistinctBy`**: Use `DistinctBy` when you need to filter unique items based on a property.
2.  **`Except` vs. `ExceptBy`**: Use `ExceptBy` for more complex filtering with key comparisons.
3.  **`Intersect` vs. `IntersectBy`**: `IntersectBy` is for matching elements using a custom key selector.
4.  **`Union` vs. `UnionBy`**: Use `UnionBy` to merge collections uniquely based on a specific property.

These operations are particularly useful when working with large datasets, such as filtering or combining data from
multiple sources while ensuring uniqueness or commonality.
