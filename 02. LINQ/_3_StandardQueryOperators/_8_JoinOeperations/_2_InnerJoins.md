### **Inner Joins in LINQ**

An **inner join** combines two data sources based on matching keys. In relational terms, this produces a result set that
includes elements from the first collection that match elements in the second collection. Non-matching elements are
excluded from the result.

The LINQ **`Join`** method (or the `join` clause in query syntax) performs inner joins.

---

### **Types of Inner Joins**

### **1\. Single Key Join**

A join based on a single property from each collection.

**Example**: Match `Teacher` objects with `Department` objects using `TeacherID`.

- **Query Syntax**:

  ```
  var query = from department in departments
              join teacher in teachers on department.TeacherID equals teacher.ID
              select new
              {
                  DepartmentName = department.Name,
                  TeacherName = $"{teacher.First} {teacher.Last}"
              };

  foreach (var result in query)
  {
      Console.WriteLine($"{result.DepartmentName} is managed by {result.TeacherName}");
  }

  ```

- **Method Syntax**:

  ```
  var query = teachers.Join(
      departments,
      teacher => teacher.ID,
      department => department.TeacherID,
      (teacher, department) => new
      {
          DepartmentName = department.Name,
          TeacherName = $"{teacher.First} {teacher.Last}"
      });

  foreach (var result in query)
  {
      Console.WriteLine($"{result.DepartmentName} is managed by {result.TeacherName}");
  }

  ```

---

### **2\. Composite Key Join**

A join based on multiple properties (e.g., `FirstName` and `LastName`).

**Example**: Determine which `Teacher` objects are also `Student` objects.

- **Query Syntax**:

  ```
  var query = from teacher in teachers
              join student in students
              on new { teacher.First, teacher.Last }
              equals new { student.FirstName, student.LastName }
              select $"{teacher.First} {teacher.Last}";

  Console.WriteLine("The following people are both teachers and students:");
  foreach (var name in query)
  {
      Console.WriteLine(name);
  }

  ```

- **Method Syntax**:

  ```
  var query = teachers.Join(
      students,
      teacher => new { teacher.First, teacher.Last },
      student => new { student.FirstName, student.LastName },
      (teacher, student) => $"{teacher.First} {teacher.Last}");

  Console.WriteLine("The following people are both teachers and students:");
  foreach (var name in query)
  {
      Console.WriteLine(name);
  }

  ```

---

### **3\. Multiple Joins**

Combining results from multiple join operations.

**Example**: Match students with their departments and the teachers managing those departments.

- **Query Syntax**:

  ```
  var query = from student in students
              join department in departments on student.DepartmentID equals department.ID
              join teacher in teachers on department.TeacherID equals teacher.ID
              select new
              {
                  StudentName = $"{student.FirstName} {student.LastName}",
                  DepartmentName = department.Name,
                  TeacherName = $"{teacher.First} {teacher.Last}"
              };

  foreach (var result in query)
  {
      Console.WriteLine($"The student \\"{result.StudentName}\\" studies in the department run by \\"{result.TeacherName}\\".");
  }

  ```

- **Method Syntax**:

  ```
  var query = students.Join(
      departments,
      student => student.DepartmentID,
      department => department.ID,
      (student, department) => new { student, department })
      .Join(
          teachers,
          sd => sd.department.TeacherID,
          teacher => teacher.ID,
          (sd, teacher) => new
          {
              StudentName = $"{sd.student.FirstName} {sd.student.LastName}",
              DepartmentName = sd.department.Name,
              TeacherName = $"{teacher.First} {teacher.Last}"
          });

  foreach (var result in query)
  {
      Console.WriteLine($"The student \\"{result.StudentName}\\" studies in the department run by \\"{result.TeacherName}\\".");
  }

  ```

---

### **4\. Inner Join Using Group Join**

Using `GroupJoin` to perform an inner join by flattening the intermediate group.

**Example**: Match students with their departments.

- **Query Syntax**:

  ```
  var query = from department in departments
              join student in students on department.ID equals student.DepartmentID into gj
              from subStudent in gj
              select new
              {
                  DepartmentName = department.Name,
                  StudentName = $"{subStudent.FirstName} {subStudent.LastName}"
              };

  foreach (var result in query)
  {
      Console.WriteLine($"{result.DepartmentName} - {result.StudentName}");
  }

  ```

- **Method Syntax**:

  ```
  var query = departments.GroupJoin(
      students,
      department => department.ID,
      student => student.DepartmentID,
      (department, gj) => new { department, gj })
      .SelectMany(
          departmentAndStudent => departmentAndStudent.gj,
          (departmentAndStudent, student) => new
          {
              DepartmentName = departmentAndStudent.department.Name,
              StudentName = $"{student.FirstName} {student.LastName}"
          });

  foreach (var result in query)
  {
      Console.WriteLine($"{result.DepartmentName} - {result.StudentName}");
  }

  ```

---

### **Comparison of Techniques**

| **Join Type**          | **Key Feature**                                                              | **When to Use**                                |
| ---------------------- | ---------------------------------------------------------------------------- | ---------------------------------------------- |
| **Single Key Join**    | Matches based on a single property.                                          | Simple relationships.                          |
| **Composite Key Join** | Matches based on multiple properties using an anonymous type as the key.     | Complex relationships requiring multiple keys. |
| **Multiple Joins**     | Chains multiple joins to correlate more than two collections.                | Queries with multiple relationships.           |
| **Group Join**         | Groups results from one collection and flattens them into a single sequence. | Simulating both inner and outer joins.         |

---

### **Summary**

- LINQ inner joins allow seamless combination of related data from different collections.
- The `Join` method is powerful for simple key matches and chaining multiple joins.
- Group joins provide additional flexibility for grouping and flattening results.
- Both query and method syntax offer equivalent functionality, with the choice depending on readability and developer
  preference.
