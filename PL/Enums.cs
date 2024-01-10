﻿using System;
using System.Collections;
using System.Collections.Generic;


namespace PL;
/// <summary>
/// enums of  engineers level
/// </summary>
internal class EngineerExperienceCollection : IEnumerable
{
    static readonly IEnumerable<BO.EngineerExperience> e_enums =
             (Enum.GetValues(typeof(BO.EngineerExperience)) as IEnumerable<BO.EngineerExperience>)!;
    public IEnumerator GetEnumerator() => e_enums.GetEnumerator();
}

